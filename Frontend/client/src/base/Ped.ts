import * as alt from 'alt-client';
import * as native from 'natives';

export class Ped {
     private static _instance: number[] = [];

     public static ClonePed(entity: alt.Player | number): number {
          const pedNumber = native.clonePed(entity, false, false, true);
          Ped._instance.push(pedNumber);
          return pedNumber;
     }

     public static DestroyPed(pedNumber: number): void {
          native.deletePed(pedNumber);
          Ped._instance.splice(Ped._instance.indexOf(pedNumber), 1);
     }

     public static DestroyAllPeds(): void {
          for (const pedNumber of Ped._instance) {
               native.deletePed(pedNumber);
          }
          Ped._instance = [];
     }
}
