import * as alt from 'alt-client';
import Events from '../../lib/Events.js';

export class Login {
     private static discordApp: string = '1146876310510129232';

     public static async getOAuthToken(): Promise<void> {
          try {
               const token: string = await alt.Discord.requestOAuth2Token(Login.discordApp);
               alt.emitServer(Events.Login.LoginUser, token);
          } catch (e) {
               alt.logError(e);
               alt.emitServer(Events.Login.FailedToGetToken);
          }
     }
}
