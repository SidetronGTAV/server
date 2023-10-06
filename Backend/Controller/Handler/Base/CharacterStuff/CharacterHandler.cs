using System.Text.Json;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Common.Dto.UserStuff;
using Common.Enums;
using Common.Models;
using Common.Models.UserStuff;
using Common.Models.UserStuff.CharacterSkin;
using Controller.Utility;
using DataAccess.DbHandler;

namespace Controller.Handler.Base.CharacterStuff;

public abstract class CharacterHandler
{
    public static async Task StartCharacterSelectorAsync(MyPlayer player, List<CharacterSmallDto> characters)
    {
        var dbCharacter = await CharacterDbHandler.GetCharacterByIdAsync(characters[0].Id);

        if (dbCharacter == null)
        {
            throw new NullReferenceException("Character is null");
        }

        CharacterSkinHandler.SetCharacterSkin(player, dbCharacter);

        player.Emit("Client:Character:Start", JsonSerializer.Serialize(characters),
            player.MaxCharacters == -1 || player.MaxCharacters >= player.Characters.Count);
    }

    public static async Task SelectCharacterAsync(MyPlayer player, int id)
    {
        var dbCharacter = await CharacterDbHandler.GetCharacterByIdAsync(id);

        if (dbCharacter?.AccountId != player.AccountId)
        {
            //TODO: Ban User
            return;
        }

        if (dbCharacter == null)
        {
            throw new NullReferenceException("Character is null");
        }

        CharacterSkinHandler.SetCharacterSkin(player, dbCharacter);
        SetCharacterDbDataToPlayer(player, dbCharacter);
        VoiceHandler.JoinGlobalVoiceChannel(player);
        if (player.IsCharacterUnconscious)
        {
            player.Health = 0;
        }
    }

    public static async Task ChangeCharacterSkinAsync(MyPlayer player, int id)
    {
        var dbCharacter = await CharacterDbHandler.GetCharacterByIdAsync(id);

        if (dbCharacter?.AccountId != player.AccountId)
        {
            //TODO: Ban User
            return;
        }

        if (dbCharacter == null)
        {
            throw new NullReferenceException("Character is null");
        }

        CharacterSkinHandler.SetCharacterSkin(player, dbCharacter);
    }

    public static async Task CreateCharacterAsync(MyPlayer player, CharacterSkin characterSkin, string firstname,
        string lastname, DateTime birthday)
    {
        var savedCharacterSkin = await CharacterDbHandler.SaveCharacterSkinAsync(characterSkin);

        Character character = new()
        {
            Id = 0,
            Firstname = firstname,
            Lastname = lastname,
            Birthday = birthday,
            CharacterSkinId = savedCharacterSkin.Id,
            AccountId = player.AccountId,
            Position = new Common.Models.Base.Position
            {
                X = GlobalPosition.NewPlayerSpawnPosition.X, Y = GlobalPosition.NewPlayerSpawnPosition.Y,
                Z = GlobalPosition.NewPlayerSpawnPosition.Z
            },
        };

        var characterId = await CharacterDbHandler.SaveCharacterAsync(character);
        var savedCharacter = await CharacterDbHandler.GetCharacterByIdAsync(characterId);
        player.Characters.Add(new CharacterSmallDto()
            { Fullname = $"{savedCharacter!.Firstname} {savedCharacter.Lastname}", Id = savedCharacter.Id });

        CharacterSkinHandler.SetCharacterSkin(player, savedCharacter);
        VoiceHandler.JoinGlobalVoiceChannel(player);
        SetCharacterDbDataToPlayer(player, savedCharacter);
    }

    private static void SetCharacterDbDataToPlayer(MyPlayer player, Character character)
    {
        player.IsInCharacterId = character.Id;
        player.IsCharacterUnconscious = character.IsCharacterUnconscious;
        player.AtCharacterUnconscious = character.AtCharacterUnconscious;
        player.Spawn(character.Position);
        player.Frozen = false;
        DimensionHandler.RemovePrivateDimension(player.Dimension);
        player.Dimension = DimensionHandler.DefaultDimension;
    }

    public static async Task ReviveCharacterAsync(MyPlayer player)
    {
        await SetCharacterAliveAsync(player);
        DimensionHandler.RemovePrivateDimension(player.Dimension);
        player.Dimension = DimensionHandler.DefaultDimension;
        player.Spawn(player.Position);
        player.ClearBloodDamage();
        player.Emit("Client:DeadHandler:Revived");
    }

    public static async Task CharacterDieAsync(MyPlayer player)
    {
        var vehicle = await CreateVehicleAndSetPlayerAndVehicleInPrivateDimensionAsync(player);
        await SetCharacterAliveAsync(player);
        player.Emit("Client:DeadHandler:Died", vehicle.Id);
        await Task.Delay(20000);
        player.Spawn(GlobalPosition.PlayerDiedSpawnPosition);
        await Task.Delay(30000);
        vehicle.Destroy();

        player.Position = GlobalPosition.HospitalSpawnPosition;
        DimensionHandler.RemovePrivateDimension(player.Dimension);
        player.Dimension = DimensionHandler.DefaultDimension;
    }

    private static async Task<IVehicle> CreateVehicleAndSetPlayerAndVehicleInPrivateDimensionAsync(IWorldObject player)
    {
        if (player == null) throw new ArgumentNullException(nameof(player));
        var privateDimension = DimensionHandler.GetPrivateDimension();
        player.Dimension = privateDimension;
        var vehicle = await AltAsync.CreateVehicle(VehicleModel.Ambulance, GlobalPosition.PlayerDiedSpawnPosition,
            new AltV.Net.Data.Rotation(0, 0, -1.929f));
        vehicle.Dimension = privateDimension;
        return vehicle;
    }

    public static async Task SetCharacterUnconsciousAsync(MyPlayer player)
    {
        var character = await CharacterDbHandler.GetCharacterByIdAsync(player.IsInCharacterId);
        if (character == null) return;

        //TODO: to 15 Minutes
        var atCharacterUnconscious = DateTime.UtcNow.AddSeconds(15);

        player.IsCharacterUnconscious = true;
        player.AtCharacterUnconscious = atCharacterUnconscious;
        character.IsCharacterUnconscious = true;
        character.AtCharacterUnconscious = atCharacterUnconscious;

        VoiceHandler.MutePlayerInAllChannels(player);

        await CharacterDbHandler.SaveCharacterAsync(character);
    }

    private static async Task SetCharacterAliveAsync(MyPlayer player)
    {
        var character = await CharacterDbHandler.GetCharacterByIdAsync(player.IsInCharacterId);
        if (character == null) return;
        character.IsCharacterUnconscious = false;
        character.AtCharacterUnconscious = null;
        player.IsCharacterUnconscious = false;
        player.AtCharacterUnconscious = null;
        VoiceHandler.ChangeVoiceVolume(player, (int)VoiceVolume.LowLevel);
        await CharacterDbHandler.SaveCharacterAsync(character);
    }
}