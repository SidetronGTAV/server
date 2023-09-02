using AltV.Net;
using AltV.Net.Async;
using Common.Models;

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
}