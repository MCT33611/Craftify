import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { SecurityComponent } from './components/security/security.component';
import { RouterOutlet } from '@angular/router';
import { PasswordResetFormComponent } from '../../components/password-reset-form/password-reset-form.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { MaterialModule } from '../../shared/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { UploaderComponent } from './components/uploader/uploader.component';


@NgModule({
  declarations: [
    EditProfileComponent,
    SecurityComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    RouterOutlet,
    PasswordResetFormComponent,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    HeaderComponent,
    FooterComponent,
    UploaderComponent
  ],
})
export class ProfileModule { }
