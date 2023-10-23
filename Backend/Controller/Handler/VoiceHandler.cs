using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Common.Enums;
using Common.Models;

namespace Controller.Handler;

public class VoiceHandler : IScript
{
    private static readonly IVoiceChannel MegaphoneRangeChannel = Alt.CreateVoiceChannel(true, 2.0f);
    private static readonly IVoiceChannel HighRangeChannel = Alt.CreateVoiceChannel(true, 1.2f);
    private static readonly IVoiceChannel MidRangeChannel = Alt.CreateVoiceChannel(true, 2.5f);
    private static readonly IVoiceChannel LowRangeChannel = Alt.CreateVoiceChannel(true, 0.1f);

    public VoiceHandler()
    {
        HighRangeChannel.Priority = 2;
        MidRangeChannel.Priority = 1;
        LowRangeChannel.Priority = 0;
        MegaphoneRangeChannel.Priority = 3;

        Alt.OnClient<MyPlayer, int>("Server:Voice:Toggle", ChangeVoiceVolume);
    }

    public static void JoinGlobalVoiceChannel(IPlayer player)
    {
        HighRangeChannel.AddPlayer(player);
        MidRangeChannel.AddPlayer(player);
        LowRangeChannel.AddPlayer(player);
        MegaphoneRangeChannel.AddPlayer(player);

        LowRangeChannel.UnmutePlayer(player);
        MidRangeChannel.MutePlayer(player);
        HighRangeChannel.MutePlayer(player);
        MegaphoneRangeChannel.MutePlayer(player);
    }

    public static void RemoveFromAllVoiceChannels(IPlayer player)
    {
        HighRangeChannel.RemovePlayer(player);
        MidRangeChannel.RemovePlayer(player);
        LowRangeChannel.RemovePlayer(player);
        MegaphoneRangeChannel.RemovePlayer(player);
    }

    public static void ChangeVoiceVolume(MyPlayer player, int distanceInt)
    {
        if (player.IsCharacterDead || player.IsCharacterUnconscious)
        {
            return;
        }

        var distance = (VoiceVolume)distanceInt;

        switch (distance)
        {
            case VoiceVolume.Mute:
                MutePlayerInAllChannels(player);
                break;
            case VoiceVolume.LowLevel when LowRangeChannel.IsPlayerMuted(player):
                MutePlayerInAllChannels(player);
                LowRangeChannel.UnmutePlayer(player);
                break;
            case VoiceVolume.MidLevel when MidRangeChannel.IsPlayerMuted(player):
                MutePlayerInAllChannels(player);
                MidRangeChannel.UnmutePlayer(player);
                break;
            case VoiceVolume.HighLevel when HighRangeChannel.IsPlayerMuted(player):
                MutePlayerInAllChannels(player);
                HighRangeChannel.UnmutePlayer(player);
                break;
        }
    }

    public static void MutePlayerInAllChannels(IPlayer player)
    {
        MidRangeChannel.MutePlayer(player);
        HighRangeChannel.MutePlayer(player);
        LowRangeChannel.MutePlayer(player);
        MegaphoneRangeChannel.MutePlayer(player);
    }
}