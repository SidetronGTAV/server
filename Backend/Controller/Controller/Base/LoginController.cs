using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using AltV.Net;
using AltV.Net.Async;
using Common.Models;
using Common.Models.Discord;
using Controller.Handler.Base;

namespace Controller.Controller.Base;

public class LoginController : IScript
{
    [AsyncClientEvent("Server:Login:LoginUser")]
    public async Task OnLoginUser(MyPlayer player, string token)
    {
        var discordUser = await LoginHandler.HandleDiscordUserAsync(token);
        if (discordUser == null)
        {
            player.Kick("Dein Discord Account wurde nicht gefunden! Bitte Wende dich an den Support!");
            return;
        }

        await LoginHandler.HandleUserLoginAsync(player, discordUser);
    }
}