import { HudConfig } from '../../Interfaces/Hud.js';
import { Webview } from '../Webview.js';
import Events from '../../lib/Events.js';

export class Hud {
     private static _hud: HudConfig = {
          hunger: 0,
          thirst: 0,
          microphone: 0,
     };

     public static ShowHud() {
          Webview.Webview.emit(Events.Hud.OpenHud);
     }

     public static HideHud() {
          Webview.Webview.emit(Events.Hud.CloseHud);
     }

     public static changeHunger(hunger: number) {
          Hud._hud.hunger = hunger;
          Hud.updateHud();
     }

     public static changeThirst(thirst: number) {
          Hud._hud.thirst = thirst;
          Hud.updateHud();
     }

     public static changeMicrophone(microphone: number) {
          Hud._hud.microphone = microphone;
          Hud.updateHud();
     }

     private static updateHud() {
          Webview.Webview.emit(Events.Hud.UpdateWebview, Hud._hud);
     }
}
