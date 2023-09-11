using Microsoft.EntityFrameworkCore;

namespace Common.Models.UserStuff.CharacterSkin;

[Owned]
public class CharacterSkinNose
{
    public float NoseWidth { get; set; }
    public float NoseHeight { get; set; }
    public float NoseLength { get; set; }
    public float NoseBone { get; set; }
    public float NoseTip { get; set; }
    public float NoseCurve { get; set; }
}