using AltV.Net.Data;
using Common.Dto.UserStuff;
using Common.Models;
using Common.Models.UserStuff;
using Common.Models.UserStuff.CharacterSkin;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbHandler;

public class CharacterDbHandler
{
    public static async Task<Character?> GetCharacterByIdAsync(int id)
    {
        await using var db = new DbContext();
        return await db.Characters.Include(ch => ch.CharacterSkin).FirstOrDefaultAsync(c => c.Id == id);
    }

    public static async Task<Character> SetCharacterAsync(Character character)
    {
        await using var db = new DbContext();
        character.Id = 0;
        var result = await db.Characters.AddAsync(character);
        await db.SaveChangesAsync();
        return result.Entity;
    }

    public static async Task<CharacterSkin> SetCharacterSkinAsync(CharacterSkin characterSkin)
    {
        await using var db = new DbContext();
        characterSkin.Id = 0;
        var result = await db.CharacterSkins.AddAsync(characterSkin);
        await db.SaveChangesAsync();
        return result.Entity;
    }

    public static void SaveCharacterPosition(int id, Position position)
    {
        using var db = new DbContext();
        var character = db.Characters.FirstOrDefault(c => c.Id == id);
        if (character == null) return;
        character.Position = new Common.Models.Base.Position { X = position.X, Y = position.Y, Z = position.Z };
        db.SaveChanges();
    }

    public static async Task SetCharacterDeadAsync(MyPlayer player)
    {
        await using var db = new DbContext();
        var character = await db.Characters.FirstOrDefaultAsync(c => c.Id == player.Id);
        if (character == null) return;
        character.IsCharacterDead = true;
        character.AtCharacterDied = DateTime.UtcNow.AddMinutes(15);
        player.IsCharacterDead = true;
        player.AtCharacterDied = character.AtCharacterDied;
        await db.SaveChangesAsync();
    }

    public static async Task SetCharacterAliveAsync(MyPlayer player)
    {
        await using var db = new DbContext();
        var character = await db.Characters.FirstOrDefaultAsync(c => c.Id == player.Id);
        if (character == null) return;
        character.IsCharacterDead = false;
        character.AtCharacterDied = null;
        player.IsCharacterDead = false;
        player.AtCharacterDied = null;
        await db.SaveChangesAsync();
    }
}