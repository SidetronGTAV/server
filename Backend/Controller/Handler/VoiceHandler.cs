using System.Data.SqlTypes;
using System.Threading.Channels;
using System.Xml;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Common.Enums;
using Common.Models;

namespace Controller.Handler;

public abstract class VoiceHandler
{
    private static readonly IVoiceChannel MegaphoneRangeChannel = Alt.CreateVoiceChannel(true, 20f);
    private static readonly IVoiceChannel HighRangeChannel = Alt.CreateVoiceChannel(true, 12f);
    private static readonly IVoiceChannel MidRangeChannel = Alt.CreateVoiceChannel(true, 5f);
    private static readonly IVoiceChannel LowRangeChannel = Alt.CreateVoiceChannel(true, 1f);
    
    public static void JoinGlobalVoiceChannel(IPlayer player)
    {
        HighRangeChannel.AddPlayer(player);
        MidRangeChannel.AddPlayer(player);
        LowRangeChannel.AddPlayer(player);
        MegaphoneRangeChannel.AddPlayer(player);
        
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
    
    public static void ChangeVoiceVolume(MyPlayer player, VoiceVolume distance)
    {
        if (player.IsCharacterDead || player.IsCharacterUnconscious)
        {
            return;
        }
        
        switch (distance)
        {
            case VoiceVolume.LowLevel when !LowRangeChannel.IsPlayerMuted(player):
                MidRangeChannel.MutePlayer(player);
                HighRangeChannel.MutePlayer(player);
                LowRangeChannel.UnmutePlayer(player);
                break;
            case VoiceVolume.MidLevel when !MidRangeChannel.IsPlayerMuted(player):
                MidRangeChannel.UnmutePlayer(player);
                HighRangeChannel.MutePlayer(player);
                LowRangeChannel.MutePlayer(player);
                break;
            case VoiceVolume.HighLevel when !HighRangeChannel.IsPlayerMuted(player):
                MidRangeChannel.MutePlayer(player);
                HighRangeChannel.UnmutePlayer(player);
                LowRangeChannel.MutePlayer(player);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(distance), distance, null);
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