using System.Text.Json;
using System.Text.Json.Serialization;
using AltV.Net;
using AltV.Net.Async;
using Common.Models;
using Common.Models.UserStuff.CharacterSkin;

namespace Controller.Controller.Base;

public class CharacterController : IScript
{
    [AsyncClientEvent("Server:Character:SelectCharacter")]
    public async Task SelectCharacterAsync(MyPlayer player, int id)
    {
        Task.Yield();
        throw new NotImplementedException();
    }

    [AsyncClientEvent("Server:Character:ChangeCharacter")]
    public async Task ChangeCharacterAsync(MyPlayer player, int id)
    {
        Task.Yield();
        throw new NotImplementedException();
    }

    [AsyncClientEvent("Server:Character:CreateCharacter")]
    public async Task OnCreateCharacterAsync(MyPlayer player, string characterSkin)
    {
        if (player.isInCharacter || player.MaxCharacters <= player.Characters.Count)
        {
            //TODO: Ban User
            return;
        }

        var characterSkinDto = JsonSerializer.Deserialize<CharacterSkin>(characterSkin);
    }
}