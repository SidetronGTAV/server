import * as alt from 'alt-client';
import Events from '../lib/Events.js';
import { KeyCode } from 'alt-shared';

export class Voice {
     private static _voiceLevel: number = 1;

     constructor() {
          alt.on(Events.alt.keyup, Voice.onKeyUp);
     }

     public static onKeyUp(key: KeyCode) {
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
}