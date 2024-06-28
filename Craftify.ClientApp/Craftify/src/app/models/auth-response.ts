import { IUser } from "./iuser";

export type AuthResponse = {user:IUser, token:string,refreshToken : string }