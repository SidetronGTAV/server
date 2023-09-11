﻿using Common.Models.UserStuff;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbHandler;

public class CharacterDbHandler
{
    public static async Task<Character?> GetCharacterByIdAsync(int id)
    {
        await using var db = new DbContext();
        return await db.Characters.FirstOrDefaultAsync(c => c.Id == id);
    }
}