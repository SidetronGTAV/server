import * as alt from 'alt-client'
import {Webview} from "./Webview.js";

export class Login {
    private static discordApp: string = "1146876310510129232";

    constructor() {
        alt.onServer('Client:Login:OpenLogin', () => {
            Webview.OpenUi('Webview:Login:OpenLogin');
        })
        Webview.Webview.on('Client:Login:HandleLogin', async () => {
            await this.getOAuthToken();
        })
    }

    private async getOAuthToken(): Promise<void> {
        try {
            const token: string = await alt.Discord.requestOAuth2Token(Login.discordApp);
            alt.log("Emit Server ")
            alt.emitServer('Server:Login:SendToken', token)
        } catch (e) {
            alt.logError(e);
        }
    }
}