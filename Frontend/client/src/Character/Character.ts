import * as alt from 'alt-client'
import * as native from "natives";
import {Player} from "../Utilities/Player.js";
import {Camera} from "../Utilities/Camera.js";
import {Webview} from "../base/Webview.js";

export class Character {
    constructor() {
        alt.onServer("Client:Character:Start", Character.StartCharacter);
        alt.onServer("Client:Character:Create", Character.CreateCharacter);
        Webview.Webview.on("Client:Character:SelectCharacter", Character.SelectCharacter);
        Webview.Webview.on("Client:Character:ChangeCharacter", Character.ChangeCharacter);
    }

    private static StartCharacter(characters: unknown[]): void {
        Character.SetNativeProperties();
        Webview.OpenUi("Webview:Character:OpenSelector", characters);
    }

    private static CreateCharacter(): void {
        Character.SetNativeProperties();
    }

    private static SelectCharacter(id: number): void {
        alt.emitServer("Server:Character:SelectCharacter", id);
    }

    private static ChangeCharacter(id: number): void {
        alt.emitServer("Server:Character:ChangeCharacter", id);
    }

    private static SetNativeProperties() {
        Player.toggleController(false);
        Player.freeze(true);
        Camera.deleteAllCam();
        const forwardVector = native.getEntityForwardVector(alt.Player.local.scriptID);
        const position: position = [-1562.5055 + forwardVector.x + 1.5, -579.6528 + forwardVector.y * 1.5, 108.50769 + 0.6];
        Camera.create(position, [0, 0, 0], 90);
        Camera.pointCamToEntity(alt.Player.local);
        Camera.renderCam(true, false, 0);
    }

}