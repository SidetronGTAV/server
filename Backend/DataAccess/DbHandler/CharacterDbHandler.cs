using AltV.Net.Data;
using Common.Dto.UserStuff;
using Common.Models;
using Common.Models.UserStuff;
using Common.Models.UserStuff.CharacterSkin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.DbHandler;

public class CharacterDbHandler
{
    public static async Task<Character?> GetCharacterByIdAsync(int id)
    {
        await using var db = new DbContext();
        return await db.Characters
            .Include(ch => ch.CharacterSkin)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public static async Task<int> SaveCharacterAsync(Character character)
    {
        await using var db = new DbContext();

        var result = character.Id == 0 ? db.Characters.Add(character) : db.Characters.Update(character);
        await db.SaveChangesAsync();
        return result.Entity.Id;
    }

    public static async Task<CharacterSkin> SaveCharacterSkinAsync(CharacterSkin characterSkin)
    {
        await using var db = new DbContext();

        var result = characterSkin.Id == 0
            ? db.CharacterSkins.Add(characterSkin)
            : db.CharacterSkins.Update(characterSkin);
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
}