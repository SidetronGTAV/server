import * as alt from 'alt-client'
import * as native from 'natives'

export abstract class Camera {
    private static _instance: number[] = [];

    public static create(pos: position, rot: position, fov: number): number {
        const camNumber = native.createCamWithParams('DEFAULT_SCRIPTED_CAMERA', ...pos, ...rot, fov, true, 0);
        Camera._instance.push(camNumber);
        return camNumber;
    }

    public static renderCam(state: boolean, ease: boolean, time: number): void {
        native.renderScriptCams(state, ease, time, true, true, 0);
    }

    public static activeCam(id: number, active: boolean): void {
        native.setCamActive(id, active);
    }

    public static deleteAllCam(): void {
        native.destroyAllCams(true);
        Camera._instance = [];
    }

    public static DestroyCam(camNumber: number): void {
        native.destroyCam(camNumber, false);
        Camera._instance.splice(Camera._instance.indexOf(camNumber), 1);
    }

    public static disableRender(): void {
        native.renderScriptCams(false, false, 0, true, true, 0);
    }

    public static pointCam(id: number, pos: position): void {
        native.pointCamAtCoord(id, ...pos);
    }

    public static pointCamToEntity(id: number, entity: alt.Entity): void {
        native.pointCamAtEntity(id, entity, 0, 0, 0, true);
    }

    public static setRot(id: number, rot: position): void {
        native.setCamRot(id, ...rot, 5);
    }
}