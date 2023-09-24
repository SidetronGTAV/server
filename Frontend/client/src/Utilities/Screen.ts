import * as native from 'natives';

export abstract class Screen {
     public static fadeOut(duration: number): void {
          native.doScreenFadeOut(duration);
     }

     public static fadeIn(duration: number): void {
          native.doScreenFadeIn(duration);
     }
}
