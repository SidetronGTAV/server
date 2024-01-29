import * as alt from 'alt-client';
import * as native from 'natives';
import { Webview } from './Webview.js';
import { VehClass } from '../Interfaces/VehicleClass.js';

export default class Vehicle {
     private static interval: number | null = null;
     private static everyTick: number | null = null;
     private static distance: number = 0;
     private static allowAirControlClass = [
          VehClass.Boat,
          VehClass.Helicopter,
          VehClass.Plane,
          VehClass.Motorcycle,
          VehClass.Cycle,
          VehClass.Train
     ];

     constructor() {
          alt.on('enteredVehicle', Vehicle.enteredVehicle);
          alt.on('leftVehicle', Vehicle.leftVehicle);
          alt.on('changedVehicleSeat', Vehicle.changeVehicleSeat);
     }

     private static enteredVehicle(vehicle: alt.Vehicle, seat: number): void {
          if (seat !== 1) return;
          Webview.Webview.emit('Webview:Speedometer:Show');
          Vehicle.startInterval();
     }

     private static changeVehicleSeat(vehicle: alt.Vehicle, oldSeat: number, seat: number) {
          if (seat !== 1) Vehicle.leftVehicle();
          else Vehicle.enteredVehicle(vehicle, seat);
     }

     private static leftVehicle(): void {
          Webview.Webview.emit('Webview:Speedometer:Hide');
          if (Vehicle.interval !== null) {
               alt.clearInterval(Vehicle.interval);
               Vehicle.interval = null;
          }
          if (Vehicle.everyTick !== null) {
               alt.clearEveryTick(Vehicle.everyTick);
               Vehicle.everyTick = null;
          }
          Vehicle.distance = 0;
     }

     private static startInterval(): void {
          if (Vehicle.interval !== null) {
               alt.clearInterval(Vehicle.interval);
               Vehicle.interval = null;
          }
          //TODO: Update distance
          Vehicle.interval = alt.setInterval(Vehicle.updateSpeedometer, 100);
          Vehicle.everyTick = alt.everyTick(Vehicle.handleVehicle);
     }

     private static handleVehicle(): void {
          const vehicle = alt.Player.local.vehicle;
          if (!vehicle || Vehicle.allowAirControlClass.includes(native.getVehicleClass(vehicle))) return;
          if (native.isEntityInAir(vehicle) || native.isEntityUpsidedown(vehicle)) {
               [59, 60, 71, 72].forEach(action => native.disableControlAction(0, action, true));
          }
     }
     public static updateSpeedometer(): void {
          const vehicle = alt.Player.local.vehicle;
          if (!vehicle) return;
          const speed = Math.round(native.getEntitySpeed(vehicle) * 3.6);
          Webview.Webview.emit('Webview:Speedometer:Update', speed, vehicle.fuelLevel, Vehicle.distance);
     }
}