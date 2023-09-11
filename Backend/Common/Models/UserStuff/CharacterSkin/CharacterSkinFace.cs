using Microsoft.EntityFrameworkCore;

namespace Common.Models.UserStuff.CharacterSkin;

[Owned]
public class CharacterSkinFace
{
    public byte ShapeFirstId { get; set; }
    public byte ShapeSecondId { get; set; }
    public byte ShapeThirdId { get; set; }
    public byte SkinFirstId { get; set; }
    public byte SkinSecondId { get; set; }
    public byte SkinThirdId { get; set; }
    public float ShapeMix { get; set; }
    public float SkinMix { get; set; }
    public float ThirdMix { get; set; }
}