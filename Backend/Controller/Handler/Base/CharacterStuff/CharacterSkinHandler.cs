﻿using AltV.Net.Elements.Entities;
using Common.Models.UserStuff;

namespace Controller.Handler.Base.CharacterStuff;

public class CharacterSkinHandler
{
    public static void SetCharacterSkin(IPlayer player, Character character)
    {
        //---------Initialize----------//
        player.SetHeadBlendData(0, 0, 0, 0, 0, 0, 0, 0, 0);

        //-----------Overlay-----------//
        player.SetHeadOverlay(0, character.CharacterSkin.Detail.Blemish, character.CharacterSkin.Detail.BlemishOpacity);
        player.SetHeadOverlay(1, character.CharacterSkin.Hairiness.Beard,
            character.CharacterSkin.Hairiness.BeardOpacity);
        player.SetHeadOverlay(2, character.CharacterSkin.Eye.EyeBrown, character.CharacterSkin.Eye.EyeBrownDense);
        player.SetHeadOverlay(3, character.CharacterSkin.Detail.Aging, character.CharacterSkin.Detail.AgingOpacity);
        player.SetHeadOverlay(4, character.CharacterSkin.Detail.Makeup, character.CharacterSkin.Detail.MakeupOpacity);
        player.SetHeadOverlay(5, character.CharacterSkin.Detail.Redness, character.CharacterSkin.Detail.RednessOpacity);
        player.SetHeadOverlay(6, character.CharacterSkin.Detail.Complexion,
            character.CharacterSkin.Detail.ComplexionOpacity);
        player.SetHeadOverlay(7, character.CharacterSkin.Detail.SunDamage,
            character.CharacterSkin.Detail.SunDamageOpacity);
        player.SetHeadOverlay(8, character.CharacterSkin.Lip.LipStick, character.CharacterSkin.Lip.LipStickOpacity);
        player.SetHeadOverlay(9, character.CharacterSkin.Detail.Moles, character.CharacterSkin.Detail.MolesOpacity);
        player.SetHeadOverlay(10, character.CharacterSkin.Hairiness.ChestHair,
            character.CharacterSkin.Hairiness.ChestHairOpacity);
        player.SetHeadOverlay(11, character.CharacterSkin.Detail.BodyBlemish,
            character.CharacterSkin.Detail.BodyBlemishOpacity);
        player.SetEyeColor(character.CharacterSkin.Eye.EyeColor);


        //---------FaceFeatures---------//
        player.SetFaceFeature(0, character.CharacterSkin.Nose.NoseWidth);
        player.SetFaceFeature(1, character.CharacterSkin.Nose.NoseHeight);
        player.SetFaceFeature(2, character.CharacterSkin.Nose.NoseLength);
        player.SetFaceFeature(3, character.CharacterSkin.Nose.NoseBone);
        player.SetFaceFeature(4, character.CharacterSkin.Nose.NoseTip);
        player.SetFaceFeature(5, character.CharacterSkin.Nose.NoseCurve);
        player.SetFaceFeature(6, character.CharacterSkin.Eye.EyeBrownHeight);
        player.SetFaceFeature(7, character.CharacterSkin.Eye.EyeBrownOffset);
        player.SetFaceFeature(8, character.CharacterSkin.Cheek.CheekHeight);
        player.SetFaceFeature(9, character.CharacterSkin.Cheek.CheekBonesWidth);
        player.SetFaceFeature(10, character.CharacterSkin.Cheek.CheekWidth);
        player.SetFaceFeature(11, character.CharacterSkin.Eye.EyeShape);
        player.SetFaceFeature(12, character.CharacterSkin.Lip.LipWidth);


        player.SetFaceFeature(13, character.CharacterSkin.MouthArea.ChimpWidth);
        player.SetFaceFeature(14, character.CharacterSkin.MouthArea.ChinForm);

        player.SetFaceFeature(15, character.CharacterSkin.MouthArea.ChinHeight);
        player.SetFaceFeature(16, character.CharacterSkin.MouthArea.ChinLength);
        player.SetFaceFeature(17, character.CharacterSkin.MouthArea.ChinWidth);
        player.SetFaceFeature(18, character.CharacterSkin.MouthArea.ChinTwist);

        player.SetFaceFeature(19, character.CharacterSkin.Neck.NeckWidth);

        //----------Hairstyle----------//
        player.SetClothes(2, character.CharacterSkin.Hairiness.Hair, 0, 0);

        //------------Colors------------//

        player.HairColor = character.CharacterSkin.Hairiness.HairColorMain;
        player.HairHighlightColor = character.CharacterSkin.Hairiness.HairColorSecond;

        player.SetHeadOverlayColor(1, 2, character.CharacterSkin.Hairiness.BeardColorMain,
            character.CharacterSkin.Hairiness.BeardColorSecond);
        player.SetHeadOverlayColor(2, 1, character.CharacterSkin.Eye.EyeBrownColorMain,
            character.CharacterSkin.Eye.EyeBrownColorSecond);
        player.SetHeadOverlayColor(8, 2, character.CharacterSkin.Lip.LipColorMain,
            character.CharacterSkin.Lip.LipColorSecond);
        player.SetHeadOverlayColor(10, 2, character.CharacterSkin.Hairiness.ChestHairColorMain,
            character.CharacterSkin.Hairiness.ChestHairColorSecond);


        //------------Finish------------//
        player.SetHeadBlendData(
            character.CharacterSkin.SkinFace.ShapeFirstId,
            character.CharacterSkin.SkinFace.ShapeSecondId,
            character.CharacterSkin.SkinFace.ShapeThirdId,
            character.CharacterSkin.SkinFace.SkinFirstId,
            character.CharacterSkin.SkinFace.SkinSecondId,
            character.CharacterSkin.SkinFace.SkinThirdId,
            character.CharacterSkin.SkinFace.ShapeMix,
            character.CharacterSkin.SkinFace.SkinMix,
            character.CharacterSkin.SkinFace.ThirdMix
        );
    }
}