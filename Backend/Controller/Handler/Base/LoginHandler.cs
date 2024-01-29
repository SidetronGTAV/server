using System.Net.Http.Headers;
using System.Net.Http.Json;
using AltV.Net.Elements.Entities;
using AltV.Net.Events;
using AutoMapper;
using Common.Dto.UserStuff;
using Common.Enums;
using Common.Enums.Logging;
using Common.Models;
using Common.Models.Discord;
using Common.Models.UserStuff;
using DataAccess;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public abstract class LoginHandler
{
    public static async Task<DiscordUser?> GetDiscordUserAsync(string token)
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

    public static async Task<List<CharacterSmallDto>?> HandleUserLoginAndLoadUserCharactersAsync(MyPlayer player,
        DiscordUser discordUser)
    {
        var account = await AccountDbHandler.GetAccountByDiscordIdAsync(discordUser.id) ??
                      await AccountHandler.CreateAccountAsync(player, discordUser);

        if (!await HandleAccountConnectPermissionsAsync(player, account)) return null;

        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Character, CharacterSmallDto>()
                .ForMember(ch => ch.Fullname, src => src.MapFrom(ch => $"{ch.Firstname} {ch.Lastname}")));
        var mapper = new Mapper(config);
        var characters = mapper.Map<List<Character>, List<CharacterSmallDto>>(account.Characters);
        SetAccountDbDataToPlayer(player, account, characters);
        return characters;
    }

    private static async Task<bool> HandleAccountConnectPermissionsAsync(IPlayer player, Account account)
    {
        if (!account.Whitelisted)
        {
            player.Kick("Du bist nicht gewhitelistet! Wende dich an den Support!");
            await LogHandler.LogAsync(LogType.Information, LogSystemType.LoginSystem, "Player tried to login but is not whitelisted!", account.Id);
        }
        else if (await BanHandler.IsPlayerBannedAsync(account))
        {
            player.Kick($"Du bist gebannt! Grund: {account.Ban.Reason}! Du kannst dich am {account.Ban.ExpirationDate} wieder einloggen!");
            await LogHandler.LogAsync(LogType.Information, LogSystemType.LoginSystem, "Player tried to login but is banned!", account.Id);
        }
        //TODO: Einkommentieren zu Release
        /*else if (account.CloudId != player.CloudId)
        {
            player.Kick("Deine Cloud Id ist falsch! Wende dich an den Support!");
            await LogHandler.LogAsync(LogType.Information, LogSystemType.LoginSystem, "Player tried to login but cloud id is wrong!", account.Id);
            
        }*/
        else if (account.HardwareIdHash != player.HardwareIdHash || account.HardwareIdExHash != player.HardwareIdExHash)
        {
            player.Kick("Deine HardwareId ist falsch! Wende dich an den Support!");
            //TODO: Ban User
        }
        else if (!await AccountDbHandler.FindOtherHardwareIdHashesSocialClubIdsAndCloudIdsAsync(account))
        {
            player.Kick("Multiaccount Sperre! Wende dich an den Support!");
            //TODO: Ban User
        }
        else
        {
            return true;
        }

        return false;
    }

    private static void SetAccountDbDataToPlayer(MyPlayer player, Account account, List<CharacterSmallDto> characters)
    {
        player.Characters = characters;
        player.MaxCharacters = account.MaxCharacters;
        player.AccountId = account.Id;
        player.AccountDiscordId = account.DiscordId;
        player.IsLoggin = true;
        player.SupportLevel = account.SupportLevel;
    }
}