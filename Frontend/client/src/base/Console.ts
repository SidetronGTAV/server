import * as alt from 'alt-client';
import * as native from 'natives';

export class Console {
     static [index: string]: (...args: string[]) => void;

     constructor() {
          alt.on('consoleCommand', (command: string, ...args) => {
               if (Console.hasOwnProperty(command)) {
                    Console[command](...args);
               }
          });
          alt.onServer('Client:Console:PlayerID', (id: number) => {
               alt.log(`Your ID: ${id}`);
          });
     }

     protected static car(vehicle: string) {
          alt.emitServer('Server:Console:SpawnVehicle', vehicle);
     }

     protected static dv(radius: string): void {
          alt.emitServer('Server:Console:DeleteVehicle', parseInt(radius));
     }

     protected static pos(): void {
          alt.log('Position: ' + alt.Player.local.pos);
     }

     protected static id(): void {
          alt.emitServer('Server:Console:PlayerID');
     }

     protected static tptoplayer(id: string): void {
          alt.emitServer('Server:Console:TpToPlayer', parseInt(id));
     }

     protected static tptome(id: string): void {
          alt.emitServer('Server:Console:TpToMe', parseInt(id));
     }

     protected static weapon(weapon: string): void {
          alt.emitServer('Server:Console:GiveWeapon', weapon);
     }

     protected static revive(id: string | null = null): void {
          alt.emitServer('Server:Console:Revive', !!id ? parseInt(id) : null);
     }

     protected static animfx(name: string) {
          native.animpostfxPlay(name, 10000, false);
     }

     protected static animfxstop() {
          native.animpostfxStopAll();
     }

     protected static test(text: string) {
          const pos = alt.Player.local.pos;
          const ret = native.getClosestVehicleNode(pos.x, pos.y, pos.z, null, 21112, 12, 1);
          alt.log(`ret type: ${typeof ret}`);
          alt.log('args', ...(Array.isArray(ret) ? ret : []));
     }

     protected static rot() {
          alt.emitServer('Server:Console:Rotation');
     }

     protected static k(vehicle: string) {
          alt.emitServer('Server:Console:SpawnVehicle', vehicle);
     }
}
