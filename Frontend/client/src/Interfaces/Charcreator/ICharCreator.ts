import { ISkinFace } from './ISkinFace';
import { IEye } from './IEye';
import { INose } from './INose';
import { ICheek } from './ICheek';
import { ILip } from './ILip';
import { IMouthArea } from './IMouthArea';
import { INeck } from './INeck';
import { IDetails } from './IDetails';
import { IHairiness } from './IHairiness';

export interface ICharCreator {
     Firstname: string;
     Lastname: string;
     Birthday: Date;
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
