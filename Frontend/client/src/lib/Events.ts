export abstract class Events {
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
     };
}
