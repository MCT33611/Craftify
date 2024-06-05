import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfilesRoutingModule } from './profiles-routing.module';
import { UsersListComponent } from './components/users-list/users-list.component';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { UiDatatableComponent } from '../../../../shared/ui/ui-datatable/ui-datatable.component';
import { UiUpsertFormComponent } from '../../../../shared/ui/ui-upsert-form/ui-upsert-form.component';


@NgModule({
  declarations: [
    UsersListComponent,
    UserDetailsComponent
  ],
  imports: [
    CommonModule,
    ProfilesRoutingModule,
    UiDatatableComponent,
    UiUpsertFormComponent
  ]
})
export class ProfilesModule { }