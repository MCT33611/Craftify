import { IUser } from "./iuser"

export interface IWorker {
  id: string;
  userId: string;
  user?: IUser;
  serviceTitle: string;
  logoUrl?: string;
  description?: string;
  certificationUrl?: string;
  skills?: string;
  hireDate: Date;
  perHourPrice: number;
  approved: boolean;
}