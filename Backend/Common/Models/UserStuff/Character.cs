using Common.Models.Base;

namespace Common.Models.UserStuff;

public class Character : IdBase
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public int AccountId { get; set; }

    public int CharacterSkinId { get; set; }

    public CharacterSkin.CharacterSkin CharacterSkin { get; set; }

    public Account Account { get; set; }
}