import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryManagementRoutingModule } from './category-management-routing.module';
import { ListComponent } from './components/list/list.component';
import { EditComponent } from './components/edit/edit.component';
import { CreateComponent } from './components/create/create.component';
import { UiDatatableComponent } from '../../../../shared/ui/ui-datatable/ui-datatable.component';
import { UiUpsertFormComponent } from '../../../../shared/ui/ui-upsert-form/ui-upsert-form.component';
import { RouterLink } from '@angular/router';


@NgModule({
  declarations: [
    ListComponent,
    EditComponent,
    CreateComponent
  ],
  imports: [
    CommonModule,
    CategoryManagementRoutingModule,
    UiDatatableComponent,
    UiUpsertFormComponent,
    RouterLink
  ]
})
export class CategoryManagementModule { }
