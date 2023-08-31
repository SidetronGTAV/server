using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using AltV.Net;
using AltV.Net.Async;
using Common.Models;
using Common.Models.Discord;

namespace Controller.Controller.Base;

public class LoginController : IScript
{
    [AsyncClientEvent("Server:Login:SendToken")]
    public async Task OnSendToken(MyPlayer player, string token)
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://discord.com/api/v8/users/@me")
        };

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await new HttpClient().SendAsync(request);
        var json = await response.Content.ReadFromJsonAsync<DiscordUser>();
    }
}