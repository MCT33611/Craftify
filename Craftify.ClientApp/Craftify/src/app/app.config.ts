import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptorService } from './core/services/auth-interceptor.service';
import { provideToastr } from 'ngx-toastr';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
    provideHttpClient(withInterceptorsFromDi()),
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true},
    provideAnimationsAsync(),
    provideToastr(),
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService
    ]
};
