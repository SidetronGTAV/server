import * as alt from 'alt-client'
import * as native from "natives";
import {Player} from "../Utilities/Player.js";
import {Camera} from "../Utilities/Camera.js";

export class Character {
    constructor() {
        alt.onServer("Client:Character:Start", Character.StartCharacter);

    }

    private static StartCharacter(characters: unknown[]): void {
        Character.SetNativeProperties();
    }

    private static SetNativeProperties() {
        Player.toggleController(false);
        Player.freeze(true);
        Camera.deleteAllCam();
        const forwardVector = native.getEntityForwardVector(alt.Player.local.scriptID);
        const position: position = [-1562.5055 -0.8, -579.6528 -0.8, 108.50769 + 0.6];
        Camera.create(position, [0, 0, 0], 90);
        Camera.pointCamToEntity(alt.Player.local);
        Camera.renderCam(true, false, 0);
    }

}