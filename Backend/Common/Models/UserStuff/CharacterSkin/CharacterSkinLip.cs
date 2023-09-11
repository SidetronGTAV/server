using Microsoft.EntityFrameworkCore;

namespace Common.Models.UserStuff.CharacterSkin;

[Owned]
public class CharacterSkinLip
{
    public byte LipStick { get; set; }
    public float LipStickOpacity { get; set; }
    public float LipWidth { get; set; }
    public byte LipColorMain { get; set; }
    public byte LipColorSecond { get; set; }
}