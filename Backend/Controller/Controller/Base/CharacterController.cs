using System.Text.Json;
using AltV.Net;
using AltV.Net.Async;
using Common.Models;
using Common.Models.UserStuff.CharacterSkin;
using Controller.Handler.Base.CharacterStuff;

namespace Controller.Controller.Base;

public class CharacterController : IScript
{
    public CharacterController()
    {
        AltAsync.OnClient<MyPlayer, int, Task>("Server:Character:SelectCharacter", SelectCharacterAsync);
        AltAsync.OnClient<MyPlayer, int, Task>("Server:Character:ChangeCharacterSkin", ChangeCharacterSkinAsync);
        AltAsync.OnClient<MyPlayer, string, string, string, string, Task>("Server:Character:CreateCharacter",
            CreateCharacterAsync);
        Alt.OnClient<MyPlayer>("Server:Character:OpenCharacterCreator", OnOpenCharacterCreatorAsync);
    }

    private static async Task SelectCharacterAsync(MyPlayer player, int id)
    {
        await CharacterHandler.SelectCharacterAsync(player, id);
    }

    private static async Task ChangeCharacterSkinAsync(MyPlayer player, int id)
    {
        await CharacterHandler.ChangeCharacterSkinAsync(player, id);
    }

    [AsyncClientEvent("Server:Character:CreateCharacter")]
    private static async Task CreateCharacterAsync(MyPlayer player, string characterSkin, string firstname,
        string lastname,
        string birthdayString)
    {
        if (player.IsInCharacterId != 0 ||
            (player.MaxCharacters <= player.Characters.Count && player.MaxCharacters != -1))
            //TODO: Ban User
            return;

        var deserializedCharacterSkin = JsonSerializer.Deserialize<CharacterSkin>(characterSkin);
        var birthday = JsonSerializer.Deserialize<DateTime>(birthdayString).Date;

        if (deserializedCharacterSkin == null) throw new NullReferenceException("CharacterSkin is null");

        await CharacterHandler.CreateCharacterAsync(player, deserializedCharacterSkin, firstname, lastname, birthday);
    }

    private static void OnOpenCharacterCreatorAsync(MyPlayer player)
    {
        if (player.IsInCharacterId != 0 ||
            (player.MaxCharacters <= player.Characters.Count && player.MaxCharacters != -1))
            //TODO: Ban User
            return;

        player.Emit("Client:Character:Create");
    }
}