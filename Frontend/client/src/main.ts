import * as alt from 'alt-client'
import {Console} from "./base/Console.js";
import {Webview} from "./base/Webview.js";
import {Login} from "./base/Login.js";
import {Character} from "./Character/Character.js";
import {Player} from "./Utilities/Player.js";


alt.on('connectionComplete', async () => {
    alt.log("ConnectionComplete");
    new Console();
    new Webview();
    new Character();
    alt.loadDefaultIpls();
    loadIpls();
    await Login.getOAuthToken();
})


function loadIpls(): void {
    alt.requestIpl("ex_sm_13_office_02b");
}