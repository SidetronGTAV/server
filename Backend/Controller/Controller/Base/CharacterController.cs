using System.Text.Json;
using System.Text.Json.Serialization;
using AltV.Net;
using AltV.Net.Async;
using Common.Models;
using Common.Models.UserStuff.CharacterSkin;
using Controller.Handler.Base;

namespace Controller.Controller.Base;

public class CharacterController : IScript
{
    [AsyncClientEvent("Server:Character:SelectCharacter")]
    public async Task OnSelectCharacterAsync(MyPlayer player, int id)
    {
        await CharacterHandler.SelectCharacterAsync(player, id);
    }

    [AsyncClientEvent("Server:Character:ChangeCharacter")]
    public static async Task OnChangeCharacterAsync(MyPlayer player, int id)
    {
        await CharacterHandler.ChangeCharacterAsync(player, id);
    }

    [AsyncClientEvent("Server:Character:CreateCharacter")]
    public async Task OnCreateCharacterAsync(MyPlayer player, string characterSkin, string firstname, string lastname,
        string birthdayString)
    {
        if (player.isInCharacterId != 0 || (player.MaxCharacters <= player.Characters.Count && player.MaxCharacters != -1))
        {
            //TODO: Ban User
            return;
        }

        var deserializedCharacterSkin = JsonSerializer.Deserialize<CharacterSkin>(characterSkin);
        var birthday = JsonSerializer.Deserialize<DateTime>(birthdayString).Date;

        if (deserializedCharacterSkin == null)
        {
            throw new NullReferenceException("CharacterSkin is null");
        }

        await CharacterHandler.CreateCharacterAsync(player, deserializedCharacterSkin, firstname, lastname, birthday);
    }

    [AsyncClientEvent("Server:Character:OpenCharacterCreator")]
    public static async Task OnOpenCharacterCreatorAsync(MyPlayer player)
    {
        if (player.isInCharacterId != 0 ||
            (player.MaxCharacters <= player.Characters.Count && player.MaxCharacters != -1))
        {
            //TODO: Ban User
            return;
        }

        player.Emit("Client:Character:Create");
    }
}