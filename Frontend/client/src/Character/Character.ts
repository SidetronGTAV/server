import * as alt from 'alt-client'
import * as native from "natives";
import {Player} from "../Utilities/Player.js";
import {Camera} from "../Utilities/Camera.js";
import {Webview} from "../base/Webview.js";

export class Character {
    private static _camera: number;

    constructor() {
        alt.onServer("Client:Character:Start", Character.StartCharacter);
        Webview.Webview.on("Client:Character:SelectCharacter", Character.SelectCharacter);
        Webview.Webview.on("Client:Character:ChangeCharacter", Character.ChangeCharacter);
        Webview.Webview.on("Client:Character:OpenCharacterCreator", Character.OpenCharacterCreator);
    }

    private static StartCharacter(characters: string, showCharacterCreator: boolean): void {
        Character.SetStartNativeProperties();
        Webview.OpenUi("Webview:Character:OpenSelector", JSON.parse(characters), showCharacterCreator);
    }

    private static SelectCharacter(id: number): void {
        Webview.CloseUi("Webview:Character:OpenSelector");
        Character.SetStopNativeProperties();
        alt.emitServer("Server:Character:SelectCharacter", id);
    }

    private static ChangeCharacter(id: number): void {
        alt.emitServer("Server:Character:ChangeCharacter", id);
    }

    private static OpenCharacterCreator(): void {
        Character.SetStopNativeProperties();
        Webview.CloseUi("Webview:Character:OpenSelector");
        alt.emitServer("Server:Character:OpenCharacterCreator");
    }

    private static SetStartNativeProperties() {
        native.doScreenFadeIn(0);
        Player.toggleController(false);
        Player.freeze(true);
        Camera.deleteAllCam();
        const forwardVector = native.getEntityForwardVector(alt.Player.local.scriptID);
        const position: position = [-1562.5055 + forwardVector.x, -579.6528 + forwardVector.y, 108.50769 + 0.6];
        Character._camera = Camera.create(position, [0, 0, 0], 90);
        const playerRot = native.getEntityRotation(Character._camera, 1);
        native.setCamRot(Character._camera, playerRot.x, playerRot.y, playerRot.z, 1);
        Camera.pointCamToEntity(Character._camera, alt.Player.local);
        Camera.renderCam(true, false, 0);

    }

    private static SetStopNativeProperties() {
        native.doScreenFadeIn(0);
        Player.toggleController(true);
        Player.freeze(false);
        Camera.deleteAllCam();
        Camera.renderCam(false, false, 0);
    }

}