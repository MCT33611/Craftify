import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegistration } from '../models/iregistration';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { environment } from '../../../../environments/environment.prod';
import { ILogin } from '../models/ilogin';
import { Token } from '@angular/compiler';
import { TokenService } from '../../../services/token.service';

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
      tap((res: { token: string }) => {
        this.tokenService.setToken(res.token);
      }),
      catchError(error => {
        console.error(error);
        throw error;
      })
    );
  }

  LoginWithGoogle(credentials: string): Observable<any> {
    return this.http.post<any>(
      `${environment.API_BASE_URL}/Authentication/LoginWithGoogle`,
      { credential: credentials },
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
    return this.http.post(`${environment.API_BASE_URL}/Authentication/SentOtp/${encodeURIComponent(email)}`, { headers: this.headers })
  }



  confirmEmail(otp: string, email: string): Observable<any> {
    return this.http.put(`${environment.API_BASE_URL}/Authentication/ConfirmEmail`, { otp, email }, { headers: this.headers }).pipe(
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
