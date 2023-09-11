using Common.Dto.UserStuff;
using Common.Models.UserStuff;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbHandler;

public class CharacterDbHandler
{
    public static async Task<Character?> GetCharacterByIdAsync(int id)
    {
        await using var db = new DbContext();
        return await db.Characters.Include(ch => ch.CharacterSkin).FirstOrDefaultAsync(c => c.Id == id);
    }
}