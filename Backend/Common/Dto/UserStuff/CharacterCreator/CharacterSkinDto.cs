namespace Common.Dto.UserStuff.CharacterCreator;

public class CharacterSkinDto
{
    public int Sex { get; set; }
        
    public CharacterSkinCheekDto Cheek { get; set; } = new();
        
    public CharacterSkinDetailDto Detail { get; set; } = new();

    public CharacterSkinEyeDto Eye { get; set; } = new();

    public CharacterSkinFaceDto SkinFace { get; set; } = new();

    public CharacterSkinHairinessDto Hairiness { get; set; } = new();

    public CharacterSkinLipDto Lip { get; set; } = new();

    public CharacterSkinMouthAreaDto MouthArea { get; set; } = new();

    public CharacterSkinNeckDto Neck { get; set; } = new();

    public CharacterSkinNoseDto Nose { get; set; } = new();
}