import * as alt from 'alt-client'
import {Console} from "./base/Console.js";
import {Webview} from "./base/Webview.js";
import {Login} from "./base/Login.js";
import {Character} from "./Character/Character.js";
import {CharCreator} from "./Character/Creator.js";

alt.on('connectionComplete', async () => {
    alt.log("ConnectionComplete");
    new Console();
    new Webview();
    new Character();
    new CharCreator();
    alt.loadDefaultIpls();
    loadIpls();
    await Login.getOAuthToken();
})


function loadIpls(): void {
    alt.requestIpl("ex_sm_13_office_02b");
}