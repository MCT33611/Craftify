import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegistration } from '../../../models/iregistration';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { environment } from '../../../../environments/environment.prod';
import { ILogin } from '../../../models/ilogin';
import { TokenService } from '../../../services/token.service';
import { handleError } from '../../../shared/utils/handleError';
import { AuthResponse } from '../../../models/auth-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(
    private _http: HttpClient,
    private _tokenService: TokenService
  ) { }

  register(user: IRegistration): Observable<AuthResponse> {
    return this._http.post<AuthResponse>(`${environment.API_BASE_URL}/Authentication/Register/`, user)
  }

  login(user: ILogin): Observable<AuthResponse> {
    return this._http.post<AuthResponse>(`${environment.API_BASE_URL}/Authentication/Login/`, user).pipe(
      tap((res: AuthResponse) => {
        this._tokenService.setToken(res.token);
      }),
      catchError((error: HttpErrorResponse) => {
        console.error(error);
        return throwError(() => error);
      })
    );
  }
  
  loginWithGoogle(credential: string): Observable<AuthResponse> {
    return this._http.post<AuthResponse>(
      `${environment.API_BASE_URL}/Authentication/LoginWithGoogle`,
      { idToken: credential },
      { headers: new HttpHeaders({'Content-Type': 'application/json'}) }
    ).pipe(
      tap((res: AuthResponse) => {
        this._tokenService.setToken(res.token);
      }),
      catchError((error: HttpErrorResponse) => {
        console.error(error);
        return throwError(() => error);
      })
    );
  }

  sentOtp(email: string): Observable<AuthResponse> {
    return this._http.post<AuthResponse>(`${environment.API_BASE_URL}/Authentication/SendOtp/${encodeURIComponent(email)}`,null).pipe(
      catchError(handleError)
    )
  }

  confirmEmail(otp: string, email: string): Observable<boolean> {
    return this._http.put<boolean>(`${environment.API_BASE_URL}/Authentication/ConfirmEmail`, { otp, email }).pipe(
      catchError(handleError)
    );
  }

  forgetPassword(email: string): Observable<{resetPasswordToken: string}> {
    return this._http.post<{resetPasswordToken: string}>(`${environment.API_BASE_URL}/Authentication/ForgotPassword/${email}`,null).pipe(
      tap((res: { resetPasswordToken: string }) => {
        this._tokenService.setPasswordResetToken(res.resetPasswordToken);
      }),
      catchError(error => {
        console.error(error);
        throw error;
      })
    );
  }

  resetPassword(email: string, password: string): Observable<Object> {
    const resetPasswordToken = this._tokenService.getPasswordResetToken();

    if (!resetPasswordToken) {
      return throwError(()=>new Error('Reset token not found'));
    }

    const body = {
      email: email,
      token: resetPasswordToken,
      newPassword: password
    };

    return this._http.put(
      `${environment.API_BASE_URL}/Authentication/ResetPassword`,
      body
    ).pipe(
      tap(() => {
        this._tokenService.removePasswordResetToken();
      }),
      catchError(handleError)
    );
  }

  isLoggedIn(): boolean {
    const token = this._tokenService.getToken();
    return token !== null && !this._tokenService.isTokenExpired();
  }

  logout(): void {
    this._tokenService.removeToken();
    
  }


}
