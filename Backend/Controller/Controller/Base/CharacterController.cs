using AltV.Net;
using AltV.Net.Async;
using Common.Dto.UserStuff.CharacterCreator;
using Common.Models;
using Newtonsoft.Json;

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
        if (player.isInCharacter)
        {
            //TODO: Ban User
            return;
        }

        var characterSkinDto = JsonConvert.DeserializeObject<CharacterSkinDto>(characterSkin);

    }
}