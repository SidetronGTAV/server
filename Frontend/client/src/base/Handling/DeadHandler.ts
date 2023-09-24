import { Webview } from '../Webview.js';
import * as alt from 'alt-client';
import { Entity } from '../Entity.js';
import * as native from 'natives';

export class DeadHandler {
     private static _interval: number = null;

     constructor() {
          alt.onServer('Client:DeadHandler:Dead', DeadHandler.dead);
          alt.onServer('Client:DeadHandler:Revived', DeadHandler.revived);
          alt.onServer('Client:DeadHandler:Died', DeadHandler.died);
     }

     private static dead() {
          Entity.ToogleControls(false);
          native.animpostfxPlay('DefaultBlinkOutro', 3000, false);
          native.animpostfxPlay('DeathFailNeutralIn', 10000, true);
          if (DeadHandler._interval !== null) alt.clearInterval(DeadHandler._interval);
          DeadHandler._interval = alt.setInterval(() => {
               native.animpostfxPlay('DefaultBlinkOutro', 3000, false);
          }, 60000);
     }

     private static revived() {
          Webview.Webview.emit('Webview:DeadScreen:State', false);
          Entity.ToogleControls(true);
          alt.clearInterval(DeadHandler._interval);
          DeadHandler._interval = null;
          native.animpostfxStopAll();
     }

     private static died(vehicleId: number) {
          Webview.Webview.emit('Webview:DeadScreen:State', true);
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
