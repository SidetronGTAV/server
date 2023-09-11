using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AltV.Net;
using AltV.Net.Async;
using Common.Dto.UserStuff;
using Common.Models;
using Common.Models.Discord;
using Controller.Handler.Base;

namespace Controller.Controller.Base;

public class LoginController : IScript
{
    [AsyncClientEvent("Server:Login:LoginUser")]
    public async Task OnLoginUser(MyPlayer player, string token)
    {
        if (player.isLoggin)
        {
            //TODO: Ban User
            return;
        }

        var discordUser = await LoginHandler.HandleDiscordUserAsync(token);
        if (discordUser == null)
        {
            player.Kick("Dein Discord Account wurde nicht gefunden! Bitte Wende dich an den Support!");
            return;
        }

        List<CharacterSmallDto> characters = await LoginHandler.HandleUserLoginAsync(player, discordUser);

        if (characters == null)
        {
            return;
        }

        if (characters.Count == 0)
        {
            player.Emit("Client:Character:Create");
            return;
        }

        await CharacterHandler.StartCharacterSelector(player, characters);
    }
}