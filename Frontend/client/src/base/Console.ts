import * as alt from 'alt-client'

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
}