import * as native from 'natives';
import * as alt from 'alt-client';

export abstract class Player {
     private static frozen = false;
     private static cursorActive = false;

     public static freeze(freeze: boolean): void {
          if (Player.frozen === freeze) return;
          Player.frozen = freeze;
          native.freezeEntityPosition(alt.Player.local, freeze);
     }

     public static toggleController(enable: boolean): void {
          if (alt.gameControlsEnabled() === enable) return;
          alt.toggleGameControls(enable);
     }

     public static toggleCursor(enable: boolean): void {
          if (Player.cursorActive === enable) return;
          Player.cursorActive = enable;
          alt.showCursor(enable);
     }

     public static toggleRadar(enable: boolean): void {
          if (native.isRadarHidden() !== enable) return;
          native.displayRadar(enable);
     }
}
