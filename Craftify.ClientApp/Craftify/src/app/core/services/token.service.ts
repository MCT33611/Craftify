import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private readonly TOKEN_KEY = 'auth_token';

  constructor(private jwtHelper: JwtHelperService) { }

  setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
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
    return userInfo ? userInfo.role : null;
  }
}
