import {ISkinFace} from './ISkinFace.js';
import {IEye} from './IEye.js';
import {INose} from './INose.js';
import {ICheek} from './ICheek.js';
import {ILip} from './ILip.js';
import {IMouthArea} from './IMouthArea.js';
import {INeck} from './INeck.js';
import {IDetails} from './IDetails.js';
import {IHairiness} from './IHairiness.js';

export interface ICharCreator {
    Sex: number;
    SkinFace: ISkinFace;
    Eye: IEye;
    Nose: INose;
    Cheek: ICheek;
    Lip: ILip;
    MouthArea: IMouthArea;
    Neck: INeck;
    Details: IDetails;
    Hairiness: IHairiness;
}
