using AltV.Net;
using AltV.Net.Async;
using Common.Enums;
using Common.Enums.Logging;
using Common.Models;
using Controller.Handler.Base;
using Controller.Handler.Base.CharacterStuff;

namespace Controller.Controller.Base;

public class LoginController : IScript
{
    public LoginController()
    {
        AltAsync.OnClient<MyPlayer, string, Task>("Server:Login:LoginUser", LoginUserAsync);
        AltAsync.OnClient<MyPlayer, Task>("Server:Login:FailedToGetToken", FailedToLoginUserAsync);
    }

    private static async Task FailedToLoginUserAsync(MyPlayer player)
    {
        if (player.AccountId != null || player.IsLoggin) return;
        
        player.Kick("Um auf den Server zu joinen, muss dein Discord im Hintergrund geöffnet sein!");
        await LogHandler.LogAsync(LogType.Information, LogSystemType.LoginSystem,
            $"Player {player.Name} failed to login with Discord Account!",
            player.AccountId);
    }
    
    private static async Task LoginUserAsync(MyPlayer player, string token)
    {
        var discordUser = await LoginHandler.GetDiscordUserAsync(token);
        if (discordUser == null)
        {
            player.Kick("Dein Discord Account wurde nicht gefunden! Bitte Wende dich an den Support!");
            return;
        }

        var findExistingPlayer = (MyPlayer?)Alt.GetAllPlayers().FirstOrDefault(p => ((MyPlayer)p).AccountDiscordId == discordUser.id);

        if (findExistingPlayer != null)
        {
            player.Kick("Dein Discord Account ist bereits eingeloggt!");
            await LogHandler.LogAsync(LogType.Information, LogSystemType.LoginSystem,
                $"Player {player.Name} tried to login with an already logged in Discord Account!",
                findExistingPlayer.AccountId);
            return;
        }

        var characters = await LoginHandler.HandleUserLoginAndLoadUserCharactersAsync(player, discordUser);

        if (characters == null) return;

        if (characters.Count == 0)
        {
            player.Emit("Client:Character:Create");
            return;
        }

        await CharacterHandler.StartCharacterSelectorAsync(player, characters);
    }
}