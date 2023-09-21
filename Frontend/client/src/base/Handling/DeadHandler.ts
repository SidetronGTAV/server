import {Webview} from "../Webview.js";
import * as alt from "alt-client";
import {Entity} from "../Entity.js";
import * as native from "natives";

export class DeadHandler {
    private static _interval: number = null;

    constructor() {
        alt.onServer("Client:DeadHandler:Dead", DeadHandler.dead)
        alt.onServer("Client:DeadHandler:Revived", DeadHandler.revived);
        alt.onServer("Client:DeadHandler:Died", DeadHandler.died);
    }

    private static dead() {
        Entity.ToogleControls(false);
        native.animpostfxPlay("DefaultBlinkOutro", 3000, false);
        native.animpostfxPlay("DeathFailNeutralIn", 10000, true);
        if (DeadHandler._interval !== null) alt.clearInterval(DeadHandler._interval);
        DeadHandler._interval = alt.setInterval(() => {
            native.animpostfxPlay("DefaultBlinkOutro", 3000, false);
        }, 60000);
    }

    private static revived() {
        Webview.Webview.emit("Webview:DeadScreen:State", false);
        Entity.ToogleControls(true);
        alt.clearInterval(DeadHandler._interval);
        DeadHandler._interval = null;
        native.animpostfxStopAll();
    }

    private static died() {
        Webview.Webview.emit("Webview:DeadScreen:State", true);
        alt.setTimeout(() => {
            const sirene = new alt.Audio("@assets/stream/audio/SIREN_WAIL_01.wav", 0.5, false);
            const output = new alt.AudioOutputWorld(new alt.Vector3(310.07, -580.10, 43.28));
            sirene.addOutput(output);
            sirene.looped = true;
            sirene.play();
            alt.setTimeout(() => {
                sirene.destroy();
                output.destroy();
                DeadHandler.revived();
                Webview.Webview.emit("Webview:DeadScreen:State", false);
            }, 20000);
        }, 20000);
    }
}