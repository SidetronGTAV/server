import * as alt from 'alt-client';
import Events from '../lib/Events.js';
import { KeyCode } from 'alt-shared';
import { Hud } from './Handling/Hud.js';

export class Voice {
     private static _voiceLevel: number = 1;
     private static _timeout: number | null = null;
     private static _marker: alt.Marker | null = null;
     private static readonly markerColor: alt.RGBA = new alt.RGBA(255, 255,255, 30);


     constructor() {
          alt.on(Events.alt.keyup, Voice.onKeyUp);
          alt.onServer(Events.Voice.UpdateMicrophoneLevel, Voice.changeHudVoiceLevel);
          alt.everyTick(Voice.everyTick);
     }

     private static onKeyUp(key: KeyCode) {
          switch (key) {
               case KeyCode['.']:
                    Voice._voiceLevel++;
                    if (Voice._voiceLevel > 3) Voice._voiceLevel = 1;
                    alt.emitServer(Events.Voice.Toggle, Voice._voiceLevel);
                    break;
               case KeyCode[',']:
                    Voice._voiceLevel = 0;
                    alt.emitServer(Events.Voice.Toggle, Voice._voiceLevel);
                    break;
          }
     }

     private static seeColshape() {
          let radius: number;
          switch (Voice._voiceLevel) {
               case 1:
                    radius = 2.6;
                    break;
               case 2:
                    radius = 10;
                    break;
               case 3:
                    radius = 25;
                    break;
          }

          Voice.destroyColshape();
          const localPlayer = alt.Player.local.pos;
          Voice._marker = new alt.Marker(alt.MarkerType.MarkerHalo, new alt.Vector3(localPlayer.x, localPlayer.y, localPlayer.z - 0.97), Voice.markerColor,false);
          Voice._marker.scale = new alt.Vector3(radius);
          Voice._marker.dimension = alt.Player.local.dimension;
          Voice._marker.visible = true;

          Voice._timeout = alt.setTimeout(() => {
               Voice._marker.destroy();
               Voice._timeout = null;
               Voice._marker = null;
          }, 370);
     }

     private static destroyColshape() {
          if (Voice._timeout) alt.clearTimeout(Voice._timeout);
          if (Voice._marker) Voice._marker.destroy();
          Voice._marker = null;
          Voice._timeout = null;
     }

     private static everyTick() {
          if (Voice._marker) {
               const localPlayer = alt.Player.local.pos;
               Voice._marker.pos = new alt.Vector3(localPlayer.x, localPlayer.y, localPlayer.z - 0.98);
          }
     }

     private static changeHudVoiceLevel(voiceLevel: number) {
          Hud.changeMicrophone(voiceLevel);
          Voice.seeColshape();
     }
}