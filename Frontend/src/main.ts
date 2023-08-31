import * as alt from 'alt-client'
import {Console} from "./base/Console.js";
import {Webview} from "./base/Webview.js";
import {Login} from "./base/Login.js";


alt.on('connectionComplete', () => {
    alt.log("ConnectionComplete");
    new Console();
    new Webview();
    new Login();
})