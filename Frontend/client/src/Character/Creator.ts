import {ICharCreator} from "../Interfaces/ICharCreator.js";
import * as native from 'natives';
import * as alt from 'alt-client';
import {Webview} from "../base/Webview.js";
import {Events} from "../lib/Events.js";
import {Camera} from "../Utilities/Camera.js";
import {Entity} from "../base/Entity.js";
import {Ped} from "../base/Ped.js";
import {System} from "../Utilities/System.js";
import {CharacterSex} from "../Enums/CharacterSex.js";

export class CharCreator {
    private static previousData: ICharCreator = {
        Firstname: "",
        Lastname: "",
        Birthday: new Date(),
        Sex: 0,
        SkinFace: {
            ShapeFirstId: 0,
            ShapeSecondId: 0,
            ShapeThirdId: 0,
            SkinFirstId: 0,
            SkinSecondId: 0,
            SkinThirdId: 0,
            ShapeMix: 0.0,
            SkinMix: 0.0,
            ThirdMix: 0.0,
        },
        Eye: {
            EyeColor: 0,
            EyeShape: 0.0,
            EyeBrown: 0,
            EyeBrownDense: 0.0,
            EyeBrownHeight: 0.0,
            EyeBrownColorMain: 0,
            EyeBrownColorSecond: 0,
            EyeBrownOffset: 0.0,
        },
        Nose: {
            NoseWidth: 0.0,
            NoseHeight: 0.0,
            NoseLength: 0.0,
            NoseBone: 0.0,
            NoseTip: 0.0,
            NoseCurve: 0.0,
        },
        Cheek: {
            CheekHeight: 0.0,
            CheekBonesWidth: 0.0,
            CheekWidth: 0.0,
        },
        Lip: {
            LipStick: 0,
            LipStickOpacity: 0.0,
            LipWidth: 0.0,
            LipColorMain: 0,
            LipColorSecond: 0,
        },
        MouthArea: {
            ChimpWidth: 0.0,
            ChinHeight: 0.0,
            ChinLength: 0.0,
            ChinForm: 0.0,
            ChinWidth: 0.0,
            ChinTwist: 0.0,
        },
        Neck: {
            NeckWidth: 0.0,
        },
        Details: {
            Blemish: 0,
            BlemishOpacity: 0.0,
            BodyBlemish: 0,
            BodyBlemishOpacity: 0.0,
            Aging: 0,
            AgingOpacity: 0.0,
            Redness: 0,
            RednessOpacity: 0.0,
            Complexion: 0,
            ComplexionOpacity: 0.0,
            SunDamage: 0,
            SunDamageOpacity: 0.0,
            Moles: 0,
            MolesOpacity: 0.0,
            Makeup: 0,
            MakeupOpacity: 0.0,
        },
        Hairiness: {
            Hair: 0,
            HairColorMain: 0,
            HairColorSecond: 0,
            Beard: 0,
            BeardOpacity: 0.0,
            BeardColorMain: 0,
            BeardColorSecond: 0,
            ChestHair: 0,
            ChestHairOpacity: 0.0,
            ChestHairColorMain: 0,
            ChestHairColorSecond: 0,
        },
    };

    private static isCharCreatorOpen: boolean;
    private static Ped: number;
    private static Cam: number;

    constructor() {
        Webview.Webview.on(Events.CharCreator.setCharCreatorProperty, CharCreator.SetProperty);
        Webview.Webview.on(Events.CharCreator.closeCharCreator, CharCreator.CloseCharCreator);
        Webview.Webview.on(Events.CharCreator.setSex, CharCreator.ChangeSex);
        alt.onServer(Events.CharCreator.openCharCreator, CharCreator.OpenCharCreator);
    }

    public static OpenCharCreator(): void {
        if (CharCreator.isCharCreatorOpen) return;
        if (!Webview.OpenUi(Events.CharCreator.handleCharCreator)) return;

        CharCreator.isCharCreatorOpen = true;
        native.doScreenFadeIn(0);

        const forwardVector = native.getEntityForwardVector(alt.Player.local.scriptID);
        const position: position = [-1562.5055 + forwardVector.x, -579.6528 + forwardVector.y, 108.50769 + 0.6];
        CharCreator.Cam = Camera.create(position, [0, 0, 0], 90);
        const playerRot = native.getEntityRotation(CharCreator.Cam, 1);
        native.setCamRot(CharCreator.Cam, playerRot.x, playerRot.y, playerRot.z, 1);
        Camera.pointCamToEntity(CharCreator.Cam, alt.Player.local);
        CharCreator.StartPed();
        Entity.Freeze(CharCreator.Ped, true);
        Entity.Freeze(alt.Player.local, true);
        Entity.Visible(alt.Player.local, false);
    }

    public static CloseCharCreator(): void {
        if (!CharCreator.isCharCreatorOpen) return;
        if (!Webview.CloseUi(Events.CharCreator.handleCharCreator)) return;

        CharCreator.isCharCreatorOpen = false;
        Camera.DestroyCam(CharCreator.Cam);
        Camera.disableRender();
        Ped.DestroyPed(CharCreator.Ped);

        Entity.Freeze(alt.Player.local, false);
        Entity.Visible(alt.Player.local, true);

        alt.emitServer(Events.CharCreator.setCharCreatorData, JSON.stringify(CharCreator.previousData), CharCreator.previousData.Firstname, CharCreator.previousData.Lastname, JSON.stringify(new Date()));
    }

    private static StartPed(): void {
        const player = alt.Player.local;

        CharCreator.Ped = Ped.ClonePed(alt.Player.local);
        Entity.Freeze(CharCreator.Ped, true);

        native.setPedHeadBlendData(CharCreator.Ped, 0, 0, 0, 0, 0, 0, 0, 0, 0, true);

        const rotationPlayer = native.getEntityRotation(player.scriptID, 2);

        native.setCamRot(CharCreator.Cam, rotationPlayer.x, rotationPlayer.y, rotationPlayer.z, 2);
        native.pointCamAtEntity(CharCreator.Cam, CharCreator.Ped, 0, 0, 0, true);
        Camera.renderCam(true, false, 0);
    }

    private static ChangeSex(index: CharacterSex): void {
        if (index === CharacterSex.Male) {
            const tick = alt.everyTick(() => {
                if (!native.hasModelLoaded(alt.hash('mp_m_freemode_01'))) {
                    native.requestModel(alt.hash('mp_m_freemode_01'));
                } else {
                    native.setPlayerModel(alt.Player.local.scriptID, alt.hash('mp_m_freemode_01'));
                    alt.clearEveryTick(tick);
                    Ped.DestroyPed(CharCreator.Ped);
                    CharCreator.StartPed();
                }
            });
        } else {
            const tick = alt.everyTick(() => {
                if (!native.hasModelLoaded(alt.hash('mp_f_freemode_01'))) {
                    native.requestModel(alt.hash('mp_f_freemode_01'));
                } else {
                    native.setPlayerModel(alt.Player.local.scriptID, alt.hash('mp_f_freemode_01'));
                    alt.clearEveryTick(tick);
                    Ped.DestroyPed(CharCreator.Ped);
                    CharCreator.StartPed();
                }
            });
        }
    }

    private static SetProperty(count: number, newValue: ICharCreator): void {
        const data = CharCreator.getChangedData(count, newValue);
        const readData = typeof data !== 'object' ? [data] : data;

        if (!readData || readData.length < 0) return;
        switch (readData[0]) {
            case 'ShapeFirstId':
            case 'ShapeSecondId':
            case 'SkinFirstId':
            case 'SkinSecondId':
            case 'ShapeMix':
            case 'SkinMix':
                native.setPedHeadBlendData(
                    CharCreator.Ped,
                    newValue.SkinFace.ShapeFirstId,
                    newValue.SkinFace.ShapeSecondId,
                    newValue.SkinFace.ShapeThirdId,
                    newValue.SkinFace.SkinFirstId,
                    newValue.SkinFace.SkinSecondId,
                    newValue.SkinFace.SkinThirdId,
                    newValue.SkinFace.ShapeMix,
                    newValue.SkinFace.SkinMix,
                    newValue.SkinFace.ThirdMix,
                    true
                );
                break;
            case 'EyeColor':
                native.setHeadBlendEyeColor(CharCreator.Ped, newValue.Eye.EyeColor);
                break;
            case 'EyeShape':
                native.setPedMicroMorph(CharCreator.Ped, 11, newValue.Eye.EyeShape);
                break;
            case 'EyeBrown':
            case 'EyeBrownDense':
                native.setPedHeadOverlay(CharCreator.Ped, 2, newValue.Eye.EyeBrown, newValue.Eye.EyeBrownDense);
                break;
            case 'EyeBrownHeight':
                native.setPedMicroMorph(CharCreator.Ped, 6, newValue.Eye.EyeBrownHeight);
                break;
            case 'EyeBrownColorMain':
            case 'EyeBrownColorSecond':
                native.setPedHeadOverlayTint(CharCreator.Ped, 2, 1, newValue.Eye.EyeBrownColorMain, newValue.Eye.EyeBrownColorSecond);
                break;
            case 'EyeBrownOffset':
                native.setPedMicroMorph(CharCreator.Ped, 7, newValue.Eye.EyeBrownOffset);
                break;
            case 'NoseWidth':
                native.setPedMicroMorph(CharCreator.Ped, 0, newValue.Nose.NoseWidth);
                break;
            case 'NoseHeight':
                native.setPedMicroMorph(CharCreator.Ped, 1, newValue.Nose.NoseHeight);
                break;
            case 'NoseLength':
                native.setPedMicroMorph(CharCreator.Ped, 2, newValue.Nose.NoseLength);
                break;
            case 'NoseBone':
                native.setPedMicroMorph(CharCreator.Ped, 3, newValue.Nose.NoseBone);
                break;
            case 'NoseTip':
                native.setPedMicroMorph(CharCreator.Ped, 4, newValue.Nose.NoseTip);
                break;
            case 'NoseCurve':
                native.setPedMicroMorph(CharCreator.Ped, 5, newValue.Nose.NoseCurve);
                break;
            case 'CheekHeight':
                native.setPedMicroMorph(CharCreator.Ped, 8, newValue.Cheek.CheekHeight);
                break;
            case 'CheekBonesWidth':
                native.setPedMicroMorph(CharCreator.Ped, 9, newValue.Cheek.CheekBonesWidth);
                break;
            case 'CheekWidth':
                native.setPedMicroMorph(CharCreator.Ped, 10, newValue.Cheek.CheekWidth);
                break;
            case 'LipStick':
            case 'LipStickOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 8, newValue.Lip.LipStick, newValue.Lip.LipStickOpacity);
                break;
            case 'LipWidth':
                native.setPedMicroMorph(CharCreator.Ped, 12, newValue.Lip.LipWidth);
                break;
            case 'LipColorMain':
            case 'LipColorSecond':
                native.setPedHeadOverlayTint(CharCreator.Ped, 8, 2, newValue.Lip.LipColorMain, newValue.Lip.LipColorSecond);
                break;
            case 'ChinLength':
                native.setPedMicroMorph(CharCreator.Ped, 16, newValue.MouthArea.ChinLength);
                break;
            case 'ChinHeight':
                native.setPedMicroMorph(CharCreator.Ped, 15, newValue.MouthArea.ChinHeight);
                break;
            case 'ChimpWidth':
                native.setPedMicroMorph(CharCreator.Ped, 13, newValue.MouthArea.ChimpWidth);
                break;
            case 'ChinForm':
                native.setPedMicroMorph(CharCreator.Ped, 14, newValue.MouthArea.ChinForm);
                break;
            case 'ChinWidth':
                native.setPedMicroMorph(CharCreator.Ped, 17, newValue.MouthArea.ChinWidth);
                break;
            case 'ChinTwist':
                native.setPedMicroMorph(CharCreator.Ped, 18, newValue.MouthArea.ChinTwist);
                break;
            case 'NeckWidth':
                native.setPedMicroMorph(CharCreator.Ped, 19, newValue.Neck.NeckWidth);
                break;
            case 'Blemish':
            case 'BlemishOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 0, newValue.Details.Blemish, newValue.Details.BlemishOpacity);
                break;
            case 'Aging':
            case 'AgingOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 3, newValue.Details.Aging, newValue.Details.AgingOpacity);
                break;
            case 'Redness':
            case 'RednessOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 5, newValue.Details.Redness, newValue.Details.RednessOpacity);
                break;
            case 'Complexion':
            case 'ComplexionOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 6, newValue.Details.Complexion, newValue.Details.ComplexionOpacity);
                break;
            case 'SunDamage':
            case 'SunDamageOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 7, newValue.Details.SunDamage, newValue.Details.SunDamageOpacity);
                break;
            case 'Moles':
            case 'MolesOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 9, newValue.Details.Moles, newValue.Details.MolesOpacity);
                break;
            case 'Makeup':
            case 'MakeupOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 4, newValue.Details.Makeup, newValue.Details.MakeupOpacity);
                break;
            case 'BodyBlemish':
            case 'BodyBlemishOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 11, newValue.Details.BodyBlemish, newValue.Details.BodyBlemishOpacity);
                break;
            case 'Hair':
                native.setPedComponentVariation(CharCreator.Ped, 2, newValue.Hairiness.Hair, 0, 0);
                break;
            case 'Beard':
            case 'BeardOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 1, newValue.Hairiness.Beard, newValue.Hairiness.BeardOpacity);
                break;
            case 'BeardColorMain':
            case 'BeardColorSecond':
                native.setPedHeadOverlayTint(CharCreator.Ped, 1, 2, newValue.Hairiness.BeardColorMain, newValue.Hairiness.BeardColorSecond);
                break;
            case 'ChestHair':
            case 'ChestHairOpacity':
                native.setPedHeadOverlay(CharCreator.Ped, 10, newValue.Hairiness.ChestHair, newValue.Hairiness.ChestHairOpacity);
                break;
            case 'ChestHairColorMain':
            case 'ChestHairColorSecond':
                native.setPedHeadOverlayTint(CharCreator.Ped, 10, 1, newValue.Hairiness.ChestHairColorMain, newValue.Hairiness.ChestHairColorSecond);
                break;
            case 'HairColorMain':
            case 'HairColorSecond':
                native.setPedHairTint(CharCreator.Ped, newValue.Hairiness.HairColorMain, newValue.Hairiness.HairColorSecond);
                break;
        }
        CharCreator.previousData = System.cloneDeep(newValue);
    }

    private static getChangedData(count: number, newValue: ICharCreator): string | null {
        let changedKeys: string | null = null;
        switch (count) {
            case 0:
                changedKeys = Object.keys(CharCreator.previousData.SkinFace).filter(
                    (key) => CharCreator.previousData.SkinFace[key] !== newValue.SkinFace[key]
                )[0];
                break;
            case 1:
                changedKeys = Object.keys(CharCreator.previousData.Eye).filter((key) => CharCreator.previousData.Eye[key] !== newValue.Eye[key])[0];
                changedKeys =
                    changedKeys == null
                        ? Object.keys(CharCreator.previousData.Nose).filter((key) => CharCreator.previousData.Nose[key] !== newValue.Nose[key])[0]
                        : changedKeys;
                changedKeys =
                    changedKeys == null
                        ? Object.keys(CharCreator.previousData.Cheek).filter((key) => CharCreator.previousData.Cheek[key] !== newValue.Cheek[key])[0]
                        : changedKeys;
                changedKeys =
                    changedKeys == null
                        ? Object.keys(CharCreator.previousData.Lip).filter((key) => CharCreator.previousData.Lip[key] !== newValue.Lip[key])[0]
                        : changedKeys;
                changedKeys =
                    changedKeys == null
                        ? Object.keys(CharCreator.previousData.MouthArea).filter(
                            (key) => CharCreator.previousData.MouthArea[key] !== newValue.MouthArea[key]
                        )[0]
                        : changedKeys;
                changedKeys =
                    changedKeys == null
                        ? Object.keys(CharCreator.previousData.Neck).filter((key) => CharCreator.previousData.Neck[key] !== newValue.Neck[key])[0]
                        : changedKeys;

                break;
            case 2:
                changedKeys = Object.keys(CharCreator.previousData.Details).filter((key) => CharCreator.previousData.Details[key] !== newValue.Details[key])[0];
                break;
            case 3:
                changedKeys = Object.keys(CharCreator.previousData.Hairiness).filter(
                    (key) => CharCreator.previousData.Hairiness[key] !== newValue.Hairiness[key]
                )[0];
                break;
        }
        return changedKeys;
    }
}