import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './pages/profile/profile.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { SecurityComponent } from './components/security/security.component';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MaterialModule } from '../../core/modules/material/material.module';
import { PasswordResetFormComponent } from '../../core/components/password-reset-form/password-reset-form.component';
import { ProfilePictureComponent } from '../../core/components/profile-picture/profile-picture.component';


@NgModule({
  declarations: [
    ProfileComponent,
    EditProfileComponent,
    SecurityComponent,
    
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    RouterLink,
    RouterOutlet,
    MaterialModule,
    PasswordResetFormComponent,
    ProfilePictureComponent
  ]
})
export class ProfileModule { }
