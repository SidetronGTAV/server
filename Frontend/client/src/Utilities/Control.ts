import * as alt from 'alt-client';
import { KeyCode } from 'alt-shared';
import Events from '../lib/Events.js';


export class Control {
     constructor() {
          alt.on('keyup', this.onKeyUp);
     }

     private onKeyUp = (key: number) => {
          if (alt.Player.local.vehicle !== null) return;
          switch(key) {
               case KeyCode.E:
                    alt.emitServer(Events.Control.PressedE);
                    break;
          }
     }
}