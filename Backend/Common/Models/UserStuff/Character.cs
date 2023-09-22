using Common.Models.Base;

namespace Common.Models.UserStuff;

public class Character : IdBase
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public DateTime Birthday { get; set; }

    public Position Position { get; set; }

    public int AccountId { get; set; }

    public Account Account { get; set; }

    public int CharacterSkinId { get; set; }

    public bool IsCharacterDead { get; set; }

    public DateTime? AtCharacterDied { get; set; }

    public CharacterSkin.CharacterSkin CharacterSkin { get; set; }
}