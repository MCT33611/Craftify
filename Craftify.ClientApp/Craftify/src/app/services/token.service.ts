import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JwtPayload } from '../models/jwt-payload';
import { Observable, throwError } from 'rxjs';
import { AuthResponse } from '../models/auth-response';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private readonly _AUTH_TOKEN_KEY = 'auth_token';
  private readonly _PASSWORD_RESET_TOKEN_KEY = 'password_reset_token';
  private readonly _REFRESH_TOKEN_KEY = 'refresh_token';

  constructor(private _jwtHelper: JwtHelperService, private _http: HttpClient) { }

  setToken(token: string): void {
    localStorage.setItem(this._AUTH_TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this._AUTH_TOKEN_KEY);
  }

  removeToken(): void {
    localStorage.removeItem(this._AUTH_TOKEN_KEY);
  }

  setRefreshToken(token: string): void {
    localStorage.setItem(this._REFRESH_TOKEN_KEY, token);
  }
  removeRefreshToken(): void {
    localStorage.removeItem(this._REFRESH_TOKEN_KEY);
  }
  getRefreshToken(): string | null {
    return localStorage.getItem(this._REFRESH_TOKEN_KEY);
  }


  isTokenExpired(): boolean {
    const token = this.getToken();
    if (!token) return true;
    
    const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }

  getUserInfo(): JwtPayload | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    let info = this._jwtHelper.decodeToken(token);
    return{
      ...info,
      role:info['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    }
  }

  getUserId(): string | null {
    const userInfo = this.getUserInfo();
    return userInfo ? userInfo.sub : null;
  }

  getWorkerId(): string | null |undefined{
    const userInfo = this.getUserInfo();    
    return userInfo ? userInfo.WorkerId : null;
  }

  getUserEmail(): string | null {
    const userInfo = this.getUserInfo();
    return userInfo ? userInfo.email : null;
  }
  getUserRole(): string | null {
    const userInfo = this.getUserInfo();
    return userInfo ? userInfo.role : null;
  }


  setPasswordResetToken(token: string): void {
    localStorage.setItem(this._PASSWORD_RESET_TOKEN_KEY, token);
  }

  

  getPasswordResetToken(): string | null {
    return localStorage.getItem(this._PASSWORD_RESET_TOKEN_KEY);
  }

  removePasswordResetToken(): void {
    localStorage.removeItem(this._PASSWORD_RESET_TOKEN_KEY);
  }

  refreshToken(): Observable<AuthResponse> {
    const refreshToken = this.getRefreshToken();
    const accessToken = this.getToken();
    const email = this.getUserEmail();
    
    if (!refreshToken || !email) {
      console.log("No refresh token or email available");
      return throwError(() => new Error('No refresh token or email available'));
    }
  
    return this._http.post<AuthResponse>(
      `${environment.API_BASE_URL}/Authentication/refresh`, 
      { email,accessToken, refreshToken }
    );
  }
}
