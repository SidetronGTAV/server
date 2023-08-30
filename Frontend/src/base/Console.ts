import * as alt from 'alt-client'

export class Console {
    static [index: string]: (...args: string[]) => void;

    constructor() {
        alt.on('consoleCommand', (command: string, ...args) => {
            if (Console.hasOwnProperty(command)) {
                Console[command](...args);
            }
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
}