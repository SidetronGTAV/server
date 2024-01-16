using System.Diagnostics;
using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Enums;
using Common.Models;

namespace Controller.Handler;

public class VoiceHandler : IScript
{
    private static readonly IVoiceChannel MegaphoneRangeChannel = Alt.CreateVoiceChannel(true, 60f);
    private static readonly IVoiceChannel HighRangeChannel = Alt.CreateVoiceChannel(true, 25f);
    private static readonly IVoiceChannel MidRangeChannel = Alt.CreateVoiceChannel(true, 10f);
    private static readonly IVoiceChannel LowRangeChannel = Alt.CreateVoiceChannel(true, 2.6f);

    public VoiceHandler()
    {
        HighRangeChannel.Priority = 1;
        MidRangeChannel.Priority = 2;
        LowRangeChannel.Priority = 3;
        MegaphoneRangeChannel.Priority = 0;

        Alt.OnClient<MyPlayer, int>("Server:Voice:Toggle", ChangeVoiceVolume);
    }

    public static void JoinGlobalVoiceChannel(IPlayer player)
    {
        HighRangeChannel.AddPlayer(player);
        MidRangeChannel.AddPlayer(player);
        LowRangeChannel.AddPlayer(player);
        MegaphoneRangeChannel.AddPlayer(player);

        LowRangeChannel.MutePlayer(player);
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
        if (player.IsCharacterDead || player.IsCharacterUnconscious) return;

        var distance = (VoiceVolume)distanceInt;
        
        switch (distance)
        {
            case VoiceVolume.Mute:
                MutePlayerInAllChannels(player);
                break;
            case VoiceVolume.LowLevel when LowRangeChannel.IsPlayerMuted(player):
                if (!MidRangeChannel.IsPlayerMuted(player)) MidRangeChannel.MutePlayer(player);
                if (!HighRangeChannel.IsPlayerMuted(player)) HighRangeChannel.MutePlayer(player);
                if (LowRangeChannel.IsPlayerMuted(player)) LowRangeChannel.UnmutePlayer(player);
                if (!MegaphoneRangeChannel.IsPlayerMuted(player)) MegaphoneRangeChannel.MutePlayer(player);
                break;
            case VoiceVolume.MidLevel when MidRangeChannel.IsPlayerMuted(player):
                if (MidRangeChannel.IsPlayerMuted(player)) MidRangeChannel.UnmutePlayer(player);
                if (!HighRangeChannel.IsPlayerMuted(player)) HighRangeChannel.MutePlayer(player);
                if (!LowRangeChannel.IsPlayerMuted(player)) LowRangeChannel.MutePlayer(player);
                if (!MegaphoneRangeChannel.IsPlayerMuted(player)) MegaphoneRangeChannel.MutePlayer(player);
                break;
            case VoiceVolume.HighLevel when HighRangeChannel.IsPlayerMuted(player):
                if (!MidRangeChannel.IsPlayerMuted(player)) MidRangeChannel.MutePlayer(player);
                if (HighRangeChannel.IsPlayerMuted(player)) HighRangeChannel.UnmutePlayer(player);
                if (!LowRangeChannel.IsPlayerMuted(player)) LowRangeChannel.MutePlayer(player);
                if (!MegaphoneRangeChannel.IsPlayerMuted(player)) MegaphoneRangeChannel.MutePlayer(player);
                break;
        }
        
        player.Emit("Server:Voice:UpdateMicrophoneLevel", (int)distance);
    }

    public static void MutePlayerInAllChannels(IPlayer player)
    {
        if (!MidRangeChannel.IsPlayerMuted(player)) MidRangeChannel.MutePlayer(player);
        if (!HighRangeChannel.IsPlayerMuted(player)) HighRangeChannel.MutePlayer(player);
        if (!LowRangeChannel.IsPlayerMuted(player)) LowRangeChannel.MutePlayer(player);
        if (!MegaphoneRangeChannel.IsPlayerMuted(player)) MegaphoneRangeChannel.MutePlayer(player);
    }
}