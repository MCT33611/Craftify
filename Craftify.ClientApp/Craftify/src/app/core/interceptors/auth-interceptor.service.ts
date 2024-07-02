import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpHeaders, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, filter, take } from 'rxjs/operators';
import { TokenService } from '../../services/token.service';
import { AuthResponse } from '../../models/auth-response';
import { AuthService } from '../../features/authentication/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(
    private tokenService: TokenService,
    private _authService: AuthService
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.tokenService.getToken();
  
    if (token) {
      request = this.addToken(request, token);
    }
  
    return next.handle(request).pipe(
      catchError(error => {
        console.log('Interceptor caught an error:', error);
        if (error instanceof HttpErrorResponse && error.status === 401) {
          console.log('401 error detected, attempting to refresh token');
          return this.handle401Error(request, next);
        }
        return throwError(() => error);
      })
    );
  }
  
  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    return this.tokenService.refreshToken().pipe(
      switchMap((res: AuthResponse) => {
        this.tokenService.setToken(res.accessToken);
        this.tokenService.setRefreshToken(res.refreshToken);
        return next.handle(this.addToken(request, res.accessToken));
      }),
      catchError(err => {

        
        this.tokenService.removeToken();
        this.tokenService.removeRefreshToken();
        this._authService.logout();
        return throwError(() => err);
      })
    );
  }

  private addToken(request: HttpRequest<any>, token: string) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'accept': '*/*'
    });

    if (!(request.body instanceof FormData)) {
      headers.set('Content-Type', 'application/json');
    }

    return request.clone({ headers });
  }


}