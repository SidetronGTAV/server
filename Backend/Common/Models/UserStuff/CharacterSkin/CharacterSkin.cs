using Common.Enums;
using Common.Models.Base;

namespace Common.Models.UserStuff.CharacterSkin;

public class CharacterSkin : IdBase
{
    public CharacterSex Sex { get; set; }

    public CharacterSkinCheek Cheek { get; set; } = new();

    public CharacterSkinDetail Detail { get; set; } = new();

    public CharacterSkinEye Eye { get; set; } = new();

    public CharacterSkinFace SkinFace { get; set; } = new();

    public CharacterSkinHairiness Hairiness { get; set; } = new();

    public CharacterSkinLip Lip { get; set; } = new();

    public CharacterSkinMouthArea MouthArea { get; set; } = new();

    public CharacterSkinNeck Neck { get; set; } = new();

    public CharacterSkinNose Nose { get; set; } = new();

    public Character Character { get; set; }
}