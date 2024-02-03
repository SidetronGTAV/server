import * as alt from 'alt-client';
import * as native from 'natives';
import { Console } from './base/Console.js';
import { Webview } from './base/Webview.js';
import { Login } from './base/Handling/Login.js';
import { Character } from './Character/Character.js';
import { CharCreator } from './Character/Creator.js';
import { DeadHandler } from './base/Handling/DeadHandler.js';
import { Voice } from './base/Voice.js';
import Vehicle from './base/Vehicle.js';
import { Control } from './Utilities/Control.js';

alt.on('connectionComplete', async () => {
     alt.log('ConnectionComplete');
     alt.loadDefaultIpls();
     loadIpls();
     new Vehicle();
     new Console();
     new Webview();
     new Character();
     new CharCreator();
     new DeadHandler();
     new Voice();
     new Control();
     setDefaultData();
     allIntervalInstance();
     await Login.getOAuthToken();
});

function loadIpls(): void {
     alt.requestIpl('ex_sm_13_office_02b');
     //pillbox
     alt.removeIpl('rc12b_destroyed');
     alt.removeIpl('rc12b_default');
     alt.removeIpl('rc12b_hospitalinterior_lod');
     alt.removeIpl('rc12b_hospitalinterior');
     alt.removeIpl('rc12b_fixed');

     let gabzPillbox = native.getInteriorAtCoords(311.2546, -592.4204, 42.32737);
     native.pinInteriorInMemory(gabzPillbox);
     native.refreshInterior(gabzPillbox);

     let gabzMrpd = native.getInteriorAtCoords(451.0129, -993.3741, 29.1718);
     native.pinInteriorInMemory(gabzMrpd);
     const data = [
          { name: 'branded_style_set' },
          { name: 'v_gabz_mrpd_rm1' },
          { name: 'v_gabz_mrpd_rm2' },
          { name: 'v_gabz_mrpd_rm3' },
          { name: 'v_gabz_mrpd_rm4' },
          { name: 'v_gabz_mrpd_rm5' },
          { name: 'v_gabz_mrpd_rm6' },
          { name: 'v_gabz_mrpd_rm7' },
          { name: 'v_gabz_mrpd_rm8' },
          { name: 'v_gabz_mrpd_rm9' },
          { name: 'v_gabz_mrpd_rm10' },
          { name: 'v_gabz_mrpd_rm11' },
          { name: 'v_gabz_mrpd_rm12' },
          { name: 'v_gabz_mrpd_rm13' },
          { name: 'v_gabz_mrpd_rm14' },
          { name: 'v_gabz_mrpd_rm15' },
          { name: 'v_gabz_mrpd_rm16' },
          { name: 'v_gabz_mrpd_rm17' },
          { name: 'v_gabz_mrpd_rm18' },
          { name: 'v_gabz_mrpd_rm19' },
          { name: 'v_gabz_mrpd_rm20' },
          { name: 'v_gabz_mrpd_rm21' },
          { name: 'v_gabz_mrpd_rm22' },
          { name: 'v_gabz_mrpd_rm23' },
          { name: 'v_gabz_mrpd_rm24' },
          { name: 'v_gabz_mrpd_rm25' },
          { name: 'v_gabz_mrpd_rm26' },
          { name: 'v_gabz_mrpd_rm27' },
          { name: 'v_gabz_mrpd_rm28' },
          { name: 'v_gabz_mrpd_rm29' },
          { name: 'v_gabz_mrpd_rm30' },
          { name: 'v_gabz_mrpd_rm31' },
     ];
     activateInterior(gabzMrpd, data);
     native.refreshInterior(gabzMrpd);
}

const activateInterior = (id: number, interiors: { name: string }[]) => {
     interiors.forEach((interior) => {
          if (!native.isInteriorEntitySetActive(id, interior.name)) {
               native.activateInteriorEntitySet(id, interior.name);
          }
     });
};

function setDefaultData() {
     const blip = new alt.PointBlip(5943.5679611650485, -6272.114833599767, 2);
     blip.alpha = 0;
     native.doScreenFadeOut(0);
     alt.setConfigFlag(alt.ConfigFlag.DisableIdleCamera, true);
     alt.setConfigFlag(alt.ConfigFlag.DisableVehicleEngineShutdownOnLeave, true);
     native.setAudioFlag('DisableFlightMusic', true);
     alt.setInterval(() => allIntervalInstance, 25000);
}

function allIntervalInstance() {
     native.setPedConfigFlag(alt.Player.local, alt.PedConfigFlag.PreventAutoShuffleToDriversSeat, true);
     native.setPedConfigFlag(alt.Player.local.scriptID, alt.PedConfigFlag.DisableStartingVehicleEngine, true);
     native.setPedConfigFlag(alt.Player.local, alt.PedConfigFlag.AllowAutoShuffleToDriversSeat, false);
     native.setPedConfigFlag(alt.Player.local, alt.PedConfigFlag.PreventAutoShuffleToTurretSeat, true);
     native.setPedConfigFlag(alt.Player.local, alt.PedConfigFlag.DontShuffleInVehicleToMakeRoom, true);
     native.invalidateCinematicVehicleIdleMode();
}

const islandCenter = new alt.Vector3(4840.571, -5174.425, 2.0);
let nearIsland = false;
alt.everyTick(() => {
     const distance = alt.Player.local.pos.distanceTo(islandCenter);

     if (nearIsland) {
          native.setRadarAsExteriorThisFrame();
          native.setRadarAsInteriorThisFrame(alt.hash('h4_fake_islandx'), 4700.0, -5145.0, 0, 0);

          if (distance >= 3000) {
               nearIsland = false;
               native.setIslandEnabled('HeistIsland', false);
               native.setScenarioGroupEnabled('Heist_Island_Peds', false);
               native.setAudioFlag('PlayerOnDLCHeist4Island', false);
               native.setAmbientZoneListStatePersistent('AZL_DLC_Hei4_Island_Zones', false, false);
               native.setAmbientZoneListStatePersistent('AZL_DLC_Hei4_Island_Disabled_Zones', false, false);
          }
     } else if (distance < 2000 && !nearIsland) {
          nearIsland = true;
          native.setIslandEnabled('HeistIsland', true);
          native.setScenarioGroupEnabled('Heist_Island_Peds', true);
          native.setAudioFlag('PlayerOnDLCHeist4Island', true);
          native.setAmbientZoneListStatePersistent('AZL_DLC_Hei4_Island_Zones', true, true);
          native.setAmbientZoneListStatePersistent('AZL_DLC_Hei4_Island_Disabled_Zones', false, true);
     }
});
