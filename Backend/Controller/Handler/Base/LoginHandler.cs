using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Models.Discord;
using DataAccess;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public class LoginHandler
{
    public static async Task<DiscordUser?> HandleDiscordUserAsync(string token)
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://discord.com/api/v8/users/@me")
        };

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await new HttpClient().SendAsync(request);
        return await response.Content.ReadFromJsonAsync<DiscordUser>();
    }

    public static async Task HandleUserLoginAsync(DiscordUser discordUser)
    {
        var account = await AccountDbHandler.GetAccountByDiscordIdAsync(discordUser.id) ?? await AccountDbHandler.CreateAccountAsync(discordUser);
    }
}