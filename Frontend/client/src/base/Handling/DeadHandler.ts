import { Webview } from '../Webview.js';
import * as alt from 'alt-client';
import { Entity } from '../Entity.js';
import * as native from 'natives';
import Events from '../../lib/Events.js';

export class DeadHandler {
     private static _interval: number = null;

     constructor() {
          alt.onServer(Events.DeadHandler.dead, DeadHandler.dead);
          alt.onServer(Events.DeadHandler.revived, DeadHandler.revived);
          alt.onServer(Events.DeadHandler.died, DeadHandler.died);
     }

     private static dead() {
          Entity.ToggleControls(false);
          native.animpostfxPlay('DefaultBlinkOutro', 3000, false);
          native.animpostfxPlay('DeathFailNeutralIn', 10000, true);
          if (DeadHandler._interval !== null) alt.clearInterval(DeadHandler._interval);
          DeadHandler._interval = alt.setInterval(() => {
               native.animpostfxPlay('DefaultBlinkOutro', 3000, false);
          }, 60000);
     }

     private static revived() {
          Webview.Webview.emit(Events.DeadHandler.WebviewDeadHandlerState, false);
          Entity.ToggleControls(true);
          alt.clearInterval(DeadHandler._interval);
          DeadHandler._interval = null;
          native.animpostfxStopAll();
     }

     private static died(vehicleId: number) {
          Webview.Webview.emit(Events.DeadHandler.WebviewDeadHandlerState, true);
          alt.setTimeout(() => {
               const vehicle = alt.Vehicle.getByRemoteID(vehicleId);
               native.setPedIntoVehicle(alt.Player.local, vehicle, -1);
               native.setVehicleEngineOn(vehicle, true, true, false);
               native.setVehicleSiren(vehicle, true);
               native.taskVehicleDriveToCoord(alt.Player.local, vehicle, 292.06, -587.72, 43.18, 200, 1, alt.hash('ambulance'), 5, 1, 2);
               alt.setTimeout(() => {
                    DeadHandler.revived();
               }, 30000);
          }, 21000);
     }
}
