export default abstract class Events {
     public static alt = {
          consoleCommand: 'consoleCommand',
          connectionComplete: 'connectionComplete',
          enteredVehicle: 'enteredVehicle',
          leftVehicle: 'leftVehicle',
          keydown: 'keydown',
          keyup: 'keyup',
          load: 'load',
          gameEntityDestroy: 'gameEntityDestroy',
          gameEntityCreate: 'gameEntityCreate',
          resourceStop: 'resourceStop',
     };

     public static CharCreator = {
          handleCharCreator: 'Webview:CharCreator:handle',
          setCharCreatorProperty: 'Client:CharCreator:setProperty',
          closeCharCreator: 'Client:CharCreator:close',
          setCharCreatorData: 'Server:Character:CreateCharacter',
          setSex: 'Client:CharCreator:setSex',
          setSexServer: 'Server:CharCreator:SetSex',
          sexIsSet: 'Client:CharCreator:SexIsSet',
          openCharCreator: 'Client:Character:Create',
          ServerOpenCharCreator: 'Server:Character:OpenCharacterCreator',
     };

     public static CharSelector = {
          startCharSelector: 'Client:Character:Start',
          selectCharacter: 'Client:Character:SelectCharacter',
          changeCharacter: 'Client:Character:ChangeCharacter',
          openCharacterCreator: 'Client:Character:OpenCharacterCreator',
          WebviewOpenCharSelector: 'Webview:Character:OpenSelector',
          ServerSelectCharacter: 'Server:Character:SelectCharacter',
          ServerChangeCharacterSkin: 'Server:Character:ChangeCharacterSkin',
     };

     public static DeadHandler = {
          dead: 'Client:DeadHandler:Dead',
          revived: 'Client:DeadHandler:Revived',
          died: 'Client:DeadHandler:Died',
          WebviewDeadHandlerState: 'Webview:DeadScreen:State',
     };

     public static Login = {
          LoginUser: 'Server:Login:LoginUser',
     };

     public static Voice = {
          Toggle: 'Server:Voice:Toggle',
     };
}
