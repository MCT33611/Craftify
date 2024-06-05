import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegistration } from '../models/iregistration';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { environment } from '../../../../environments/environment.prod';
import { ILogin } from '../models/ilogin';
import { TokenService } from '../../../services/token.service';
import { routes } from '../../../app.routes';
import { handleError } from '../../../shared/utils/handleError';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(
    private http: HttpClient,
    private tokenService: TokenService
  ) { }

  register(user: IRegistration): Observable<any> {
    return this.http.post(`${environment.API_BASE_URL}/Authentication/Register/`, user, { headers: this.headers })
  }

  login(user: ILogin): Observable<any> {
    return this.http.post<any>(`${environment.API_BASE_URL}/Authentication/Login/`, user, { headers: this.headers }).pipe(
      tap((res: { token: string,user:any }) => {
        this.tokenService.setToken(res.token);
      }),
      catchError(error => {
        console.error(error);
        throw error;
      })
    );
  }

  LoginWithGoogle(credential: string): Observable<any> {
    return this.http.post<any>(
      `${environment.API_BASE_URL}/Authentication/LoginWithGoogle`,
      JSON.stringify(credential),
      { headers: this.headers }
    ).pipe(
      tap((res: { token: string }) => {
        this.tokenService.setToken(res.token);
      }),
      catchError(error => {
        console.error(error);
        return throwError(error);
      })
    );
  }

  sentOtp(email: string): Observable<any> {
    return this.http.post(`${environment.API_BASE_URL}/Authentication/SendOtp/${encodeURIComponent(email)}`, { headers: this.headers }).pipe(
      catchError(handleError)
    )
  }

  confirmEmail(otp: string, email: string): Observable<any> {
    return this.http.put(`${environment.API_BASE_URL}/Authentication/ConfirmEmail`, { otp, email }, { headers: this.headers }).pipe(
      catchError(handleError)
    );
  }

  forgetPassword(email: string): Observable<any> {
    return this.http.post<any>(`${environment.API_BASE_URL}/Authentication/ForgotPassword/${email}`, { headers: this.headers }).pipe(
      tap((res: { resetToken: string }) => {
        this.tokenService.setPasswordResetToken(res.resetToken);
      }),
      catchError(error => {
        console.error(error);
        throw error;
      })
    );
  }

  resetPassword(email: string, password: string): Observable<any> {
    const resetToken = this.tokenService.getPasswordResetToken();

    if (!resetToken) {
      return throwError(new Error('Reset token not found'));
    }

    const body = {
      email: email,
      token: resetToken,
      newPassword: password
    };

    return this.http.put<any>(
      `${environment.API_BASE_URL}/Authentication/ResetPassword`,
      body,
      { headers: this.headers }
    ).pipe(
      tap(() => {
        this.tokenService.removePasswordResetToken();
      }),
      catchError(handleError)
    );
  }

  isLoggedIn(): boolean {
    const token = this.tokenService.getToken();
    return token !== null && !this.tokenService.isTokenExpired();
  }

  logout(): void {
    this.tokenService.removeToken();
    
  }


}
