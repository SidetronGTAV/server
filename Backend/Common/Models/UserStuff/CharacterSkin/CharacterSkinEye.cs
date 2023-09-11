using Microsoft.EntityFrameworkCore;

namespace Common.Models.UserStuff.CharacterSkin;

[Owned]
public class CharacterSkinEye
{
    public byte EyeColor { get; set; }
    public float EyeShape { get; set; }
    public byte EyeBrown { get; set; }
    public float EyeBrownDense { get; set; }
    public float EyeBrownHeight { get; set; }
    public byte EyeBrownColorMain { get; set; }
    public byte EyeBrownColorSecond { get; set; }
    public float EyeBrownOffset { get; set; }
}