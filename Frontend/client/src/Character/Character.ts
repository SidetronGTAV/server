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
    }

    private static StartCharacter(characters: string): void {
        Character.SetStartNativeProperties();
        Webview.OpenUi("Webview:Character:OpenSelector", JSON.parse(characters));
    }

    private static SelectCharacter(id: number): void {
        Webview.CloseUi("Webview:Character:OpenSelector");
        Character.SetStopNativeProperties();
        alt.emitServer("Server:Character:SelectCharacter", id);
    }

    private static ChangeCharacter(id: number): void {
        alt.emitServer("Server:Character:ChangeCharacter", id);
    }

    private static SetStartNativeProperties() {
        Player.toggleController(false);
        Player.freeze(true);
        Camera.deleteAllCam();
        const forwardVector = native.getEntityForwardVector(alt.Player.local.scriptID);
        const position: position = [-1562.5055 + forwardVector.x + 1.5, -579.6528 + forwardVector.y * 1.5, 108.50769 + 0.6];
        Character._camera = Camera.create(position, [0, 0, 0], 90);
        const camRot = native.getCamRot(Character._camera, 2);
        native.setEntityRotation(alt.Player.local.scriptID, camRot.x, camRot.y, camRot.z, 2, true);
        Camera.pointCamToEntity(Character._camera, alt.Player.local);
        Camera.renderCam(true, false, 0);
    }

    private static SetStopNativeProperties() {
        Player.toggleController(true);
        Player.freeze(false);
        Camera.deleteAllCam();
        Camera.renderCam(false, false, 0);
    }

}