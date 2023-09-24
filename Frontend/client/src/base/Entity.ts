import * as alt from 'alt-client';
import * as native from 'natives';

export class Entity {
     public static Freeze(entity: alt.Entity | number, freeze: boolean): void {
          native.freezeEntityPosition(entity, freeze);
     }

     public static Visible(entity: alt.Entity | number, visible: boolean): void {
          native.setEntityVisible(entity, visible, false);
     }

     public static ToggleControls(toggle: boolean): void {
          alt.toggleGameControls(toggle);
     }
}
