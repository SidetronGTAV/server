using System.Collections.Concurrent;
using System.Timers;
using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Models;
using Controller.Handler.Base.CharacterStuff;
using DataAccess.DbHandler;

namespace Controller.Handler.Base;

public class ShitSaver
{
    private const int PlayersPerTick = 10;
    private const int TickInterval = 1000;

    private readonly ConcurrentQueue<IPlayer> _allFuckingPlayers = new();

    public ShitSaver()
    {
        var t = new System.Timers.Timer(TickInterval);
        t.Elapsed += OnSaveShit;
        t.Start();
    }

    private void OnSaveShit(object? source, ElapsedEventArgs e)
    {
        if (_allFuckingPlayers.IsEmpty)
        {
            foreach (var player in Alt.GetAllPlayers())
            {
                if (player is { Exists: true }) _allFuckingPlayers.Enqueue(player);
            }
        }

        for (var i = 0; i < PlayersPerTick; i++)
        {
            if (!_allFuckingPlayers.TryDequeue(out var player)) return;
            if (player is { Exists: false }) continue;
            var myPlayer = (MyPlayer)player;
            if (myPlayer.IsInCharacterId == 0) continue;
            if (myPlayer.AtCharacterUnconscious < DateTime.UtcNow)
            {
                Task.Run(() => CharacterHandler.CharacterDieAsync(myPlayer));
            }

            CharacterDbHandler.SaveCharacterPosition(myPlayer.IsInCharacterId, player.Position);
        }
    }
}