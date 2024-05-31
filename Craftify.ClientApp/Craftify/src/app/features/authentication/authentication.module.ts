import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { RouterOutlet } from '@angular/router';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GoogleLoginButtonComponent } from '../../components/google-login-button/google-login-button.component';
import { OtpComponent } from './components/otp/otp.component';


@NgModule({
  declarations: [
    OtpComponent,
    SignInComponent,
    SignUpComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    RouterOutlet,
    ReactiveFormsModule,
    FormsModule,
    GoogleLoginButtonComponent
  ]
})
export class AuthenticationModule { }
