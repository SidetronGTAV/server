import * as alt from 'alt-client';
import * as native from 'natives';
import { Webview } from './Webview.js';

export default class Vehicle {
     private static interval : number | null = null;
     private static distance : number = 0;

     constructor() {
          alt.on('enteredVehicle', Vehicle.enteredVehicle);
          alt.on('leftVehicle', Vehicle.leftVehicle);
     }

     private static enteredVehicle(vehicle: alt.Vehicle, seat: number): void {
          if (seat !== 1) return;
          Webview.Webview.emit('Webview:Speedometer:Show');
          Vehicle.startInterval();
     }

     private static leftVehicle(): void {
          Webview.Webview.emit('Webview:Speedometer:Hide');
          alt.clearInterval(Vehicle.interval);
          Vehicle.interval = null;
     }

     private static startInterval(): void {
          if (Vehicle.interval !== null) {
               alt.clearInterval(Vehicle.interval);
               Vehicle.interval = null;
          }
          //TODO: Update distance
          Vehicle.interval = alt.setInterval(Vehicle.updateSpeedometer, 100);
     }

     private static updateSpeedometer(): void {
          const vehicle = alt.Player.local.vehicle;
          if (vehicle === null) return;
          const speed = (native.getEntitySpeed(vehicle) * 3.6).toString();
          const fuel = vehicle.fuelLevel;
          Webview.Webview.emit('Webview:Speedometer:Update', parseInt(speed), fuel, Vehicle.distance);
     }
}