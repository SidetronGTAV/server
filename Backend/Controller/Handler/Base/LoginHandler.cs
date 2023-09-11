using System.Net.Http.Headers;
using System.Net.Http.Json;
using AutoMapper;
using Common.Dto.UserStuff;
using Common.Models;
using Common.Models.Discord;
using Common.Models.UserStuff;
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

    public static async Task<List<CharacterSmallDto>> HandleUserLoginAsync(MyPlayer player, DiscordUser discordUser)
    {
        var account = await AccountDbHandler.GetAccountByDiscordIdAsync(discordUser.id) ??
                      await AccountDbHandler.CreateAccountAsync(player, discordUser);

        if (!account.Whitelisted)
        {
            player.Kick("Du bist nicht gewhitelistet! Wende dich an den Support!");
            return null;
        }

        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Character, CharacterSmallDto>().ForMember(ch => ch.Fullname,
                src => src.MapFrom(ch => $"{ch.Firstname} {ch.Lastname}")));
        var mapper = new Mapper(config);
        var characters = mapper.Map<List<Character>, List<CharacterSmallDto>>(account.Characters);
        SetAccountData(player, account, characters);
        return characters;
    }

    private static void SetAccountData(MyPlayer player, Account account, List<CharacterSmallDto> characters)
    {
        player.Characters = characters;
        player.MaxCharacters = account.MaxCharacters;
        player.AccountId = account.Id;
        player.AccountDiscordId = account.DiscordId;
        player.isLoggin = true;
        player.Emit("Client:Login:LoginSuccess");
    }
}