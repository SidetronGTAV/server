import * as alt from 'alt-client'
import {Console} from "./base/Console.js";


alt.on('connectionComplete', () => {
    alt.log("ConnectionComplete");
    new Console();
})