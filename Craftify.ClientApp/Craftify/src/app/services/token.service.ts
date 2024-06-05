import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private readonly AUTH_TOKEN_KEY = 'auth_token';
  private readonly PASSWORD_RESET_TOKEN_KEY = 'password_reset_token';

  constructor(private jwtHelper: JwtHelperService) { }

  setToken(token: string): void {
    localStorage.setItem(this.AUTH_TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.AUTH_TOKEN_KEY);
  }

  removeToken(): void {
    localStorage.removeItem(this.AUTH_TOKEN_KEY);
  }

  isTokenExpired(): boolean {
    const token = this.getToken();
    return token ? this.jwtHelper.isTokenExpired(token) : true;
  }

  getUserInfo(): any {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    return this.jwtHelper.decodeToken(token);
  }

  getUserId(): string | null {
    const userInfo = this.getUserInfo();
    return userInfo ? userInfo.sub : null;
  }

  getUserEmail(): string | null {
    const userInfo = this.getUserInfo();
    return userInfo ? userInfo.email : null;
  }
  getUserRole(): string | null {
    const userInfo = this.getUserInfo();
    return userInfo ? userInfo['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] : null;
  }


  setPasswordResetToken(token: string): void {
    localStorage.setItem(this.PASSWORD_RESET_TOKEN_KEY, token);
  }

  

  getPasswordResetToken(): string | null {
    return localStorage.getItem(this.PASSWORD_RESET_TOKEN_KEY);
  }

  removePasswordResetToken(): void {
    localStorage.removeItem(this.PASSWORD_RESET_TOKEN_KEY);
  }
}
