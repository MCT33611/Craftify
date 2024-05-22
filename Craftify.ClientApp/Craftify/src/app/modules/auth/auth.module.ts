import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { AuthComponent } from './pages/auth/auth.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { PasswordResetFormComponent } from '../../core/components/password-reset-form/password-reset-form.component';
import { MaterialModule } from '../../core/modules/material/material.module';
import { GoogleLoginButtonComponent } from '../../core/components/google-login-button/google-login-button.component';


@NgModule({
  declarations: [
    SignInComponent,
    SignUpComponent,
    ForgotPasswordComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    FormsModule,
    PasswordResetFormComponent,
    GoogleLoginButtonComponent
  ],
})
export class AuthModule { }
