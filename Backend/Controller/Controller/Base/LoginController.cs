using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Common.Dto.UserStuff;
using Common.Models;
using Common.Models.Discord;
using Controller.Handler.Base;
using Controller.Handler.Base.CharacterStuff;

namespace Controller.Controller.Base;

public class LoginController : IScript
{
    public LoginController()
    {
        AltAsync.OnClient<MyPlayer, string, Task>("Server:Login:LoginUser", LoginUserAsync);
    }

    private static async Task LoginUserAsync(MyPlayer player, string token)
    {
        if (player.IsLoggin)
        {
            //TODO: Kick User
            return;
        }

        var discordUser = await LoginHandler.GetDiscordUserAsync(token);
        if (discordUser == null)
        {
            player.Kick("Dein Discord Account wurde nicht gefunden! Bitte Wende dich an den Support!");
            return;
        }

        var characters = await LoginHandler.HandleUserLoginAndLoadUserCharactersAsync(player, discordUser);

        if (characters == null)
        {
            return;
        }

        if (characters.Count == 0)
        {
            player.Emit("Client:Character:Create");
            return;
        }

        await CharacterHandler.StartCharacterSelectorAsync(player, characters);
    }
}