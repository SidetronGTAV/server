using Microsoft.EntityFrameworkCore;

namespace Common.Models.UserStuff.CharacterSkin;

[Owned]
public class CharacterSkinCheek
{
    public float CheekHeight { get; set; }
    public float CheekBonesWidth { get; set; }
    public float CheekWidth { get; set; }
}