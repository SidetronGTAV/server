using System.Collections.Concurrent;
using System.Timers;
using AltV.Net;
using AltV.Net.Elements.Entities;
using Common.Models;
using DataAccess.DbHandler;

public class ShitSaver
{
    private const int PLAYERS_PER_TICK = 10;
    private const int TICK_INTERVAL = 1000;
    
    private readonly ConcurrentQueue<IPlayer> _allFuckingPlayers = new();

    public ShitSaver()
    {
        var t = new System.Timers.Timer(TICK_INTERVAL);
        t.Elapsed += OnSaveShit;
        t.Start();
    }

    private void OnSaveShit(object? source, ElapsedEventArgs e)
    {
        if (_allFuckingPlayers.IsEmpty)
        {
            foreach (var player in Alt.GetAllPlayers())
            {
                if (player.Exists) _allFuckingPlayers.Enqueue(player);
            }
        }

        for (var i = 0; i < PLAYERS_PER_TICK; i++)
        {
            if (!_allFuckingPlayers.TryDequeue(out var player)) return;
            if (!player.Exists) continue;
            var myPlayer = (MyPlayer)player;
            if (myPlayer.isInCharacterId == 0) continue;
            CharacterDbHandler.SaveCharacterPosition(myPlayer.isInCharacterId, player.Position);
        }
    }
}