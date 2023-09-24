import * as alt from 'alt-client';
import * as native from 'natives';
import { Player } from '../Utilities/Player.js';
import { Camera } from '../Utilities/Camera.js';
import { Webview } from '../base/Webview.js';
import Events from '../lib/Events.js';

export class Character {
     private static _camera: number;

     constructor() {
          alt.onServer(Events.CharSelector.startCharSelector, Character.StartCharacter);
          Webview.Webview.on(Events.CharSelector.selectCharacter, Character.SelectCharacter);
          Webview.Webview.on(Events.CharSelector.changeCharacter, Character.ChangeCharacter);
          Webview.Webview.on(Events.CharSelector.openCharacterCreator, Character.OpenCharacterCreator);
     }

     private static StartCharacter(characters: string, showCharacterCreator: boolean): void {
          Character.SetStartNativeProperties();
          Webview.OpenUi(Events.CharSelector.WebviewOpenCharSelector, JSON.parse(characters), showCharacterCreator);
     }

     private static SelectCharacter(id: number): void {
          Webview.CloseUi(Events.CharSelector.WebviewOpenCharSelector);
          Character.SetStopNativeProperties();
          alt.emitServer(Events.CharSelector.ServerSelectCharacter, id);
     }

     private static ChangeCharacter(id: number): void {
          alt.emitServer(Events.CharSelector.ServerChangeCharacterSkin, id);
     }

     private static OpenCharacterCreator(): void {
          Character.SetStopNativeProperties();
          Webview.CloseUi(Events.CharSelector.WebviewOpenCharSelector);
          alt.emitServer(Events.CharCreator.ServerOpenCharCreator);
     }

     private static SetStartNativeProperties() {
          native.doScreenFadeIn(0);
          Player.toggleController(false);
          Player.freeze(true);
          Camera.deleteAllCam();
          const forwardVector = native.getEntityForwardVector(alt.Player.local);
          const position: position = [-1562.5055 + forwardVector.x, -579.6528 + forwardVector.y, 108.50769 + 0.6];
          Character._camera = Camera.create(position, [0, 0, 0], 90);
          const playerRot = native.getEntityRotation(Character._camera, 1);
          native.setCamRot(Character._camera, playerRot.x, playerRot.y, playerRot.z, 1);
          Camera.pointCamToEntity(Character._camera, alt.Player.local);
          Camera.renderCam(true, false, 0);
     }

     private static SetStopNativeProperties() {
          Player.toggleController(true);
          Player.freeze(false);
          Camera.deleteAllCam();
          Camera.renderCam(false, false, 0);
     }
}