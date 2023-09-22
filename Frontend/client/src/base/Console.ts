import * as alt from 'alt-client'
import * as native from 'natives'

export class Console {
    static [index: string]: (...args: string[]) => void;

    constructor() {
        alt.on('consoleCommand', (command: string, ...args) => {
            if (Console.hasOwnProperty(command)) {
                Console[command](...args);
            }
        });
        alt.onServer("Client:Console:PlayerID", (id: number) => {
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
        alt.emitServer('Server:Console:PlayerPosition');
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
        const vehicle = native.createVehicle(alt.hash("ambulance"), alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z, 0, true, true, false);
        native.setSirenWithNoDriver(vehicle, true);
    }

    protected static rot() {
        alt.emitServer("Server:Console:Rotation");
    }
}