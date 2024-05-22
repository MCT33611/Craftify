import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { environment } from '@env/environment.prod';
import { ILogin } from '../../../core/models/ilogin';
import { TokenService } from '../../../core/services/token.service';
import { IRegistration } from '../../../core/models/iregistration';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  constructor(
    private http : HttpClient,
    private tokenService : TokenService
  ) { }

  register(user: IRegistration): Observable<any> {
    return this.http.post(`${environment.API_BASE_URL}/Authentication/Register/`, user, { headers: this.headers }).pipe(
      catchError(this.handleError)
    );
  }

  login(user: ILogin): Observable<any> {
    return this.http.post<any>(`${environment.API_BASE_URL}/Authentication/Login/`, user, { headers: this.headers }).pipe(
      tap((res: { token: string }) => {
        this.tokenService.setToken(res.token);
      }),
      catchError(this.handleError)
    );
  }

  confirmEmail(otp: string, email: string): Observable<any> {
    return this.http.put(`${environment.API_BASE_URL}/Authentication/ConfirmEmail`, { otp, email }, { headers: this.headers }).pipe(
      catchError(this.handleError)
    );
  }

  loginWithGoogle(credentials: string): Observable<any> {
    return this.http.post<any>(
      `${environment.API_BASE_URL}/Authentication/LoginWithGoogle?credential=${credentials}`,
      {}, // empty body
      { headers: this.headers }
    ).pipe(
      tap((res: { token: string }) => {
        this.tokenService.setToken(res.token);
      }),
      catchError(this.handleError)
    );
  }

  private readonly PASSWORD_RESET_TOKEN_KEY = 'password_reset_token';
  forgetPassword(email: string): Observable<any> {
    return this.http.post<any>(`${environment.API_BASE_URL}/Authentication/ForgotPassword/${ email }`, { headers: this.headers }).pipe(
      tap((res: { restToken: string }) => {
        localStorage.setItem(this.PASSWORD_RESET_TOKEN_KEY, res.restToken);
      }),
      catchError(error => {
        console.error(error);
        throw error;
      })
    );
  }

  resetPassword(email: string, password: string): Observable<any> {
    const resetToken = localStorage.getItem(this.PASSWORD_RESET_TOKEN_KEY);
    if (!resetToken) {
      return throwError(new Error('Reset token not found'));
    }
    return this.http.put<any>(
      `${environment.API_BASE_URL}/Authentication/ResetPassword/`,
      { email, password, token: resetToken },
      { headers: this.headers }
    ).pipe(
      tap(() => {
        localStorage.removeItem(this.PASSWORD_RESET_TOKEN_KEY);
      }),
      catchError(this.handleError)
    );
  }








  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    // You can customize error handling here, like displaying a toast message
    // or logging errors to the console.
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
