import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WorkerRoutingModule } from './worker-routing.module';
import { MaterialModule } from '../../core/modules/material/material.module';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { ServiceFormComponent } from './components/service-form/service-form.component';


@NgModule({
  declarations: [
    ServiceListComponent,
    ServiceFormComponent
  ],
  imports: [
    CommonModule,
    WorkerRoutingModule,
    MaterialModule
  ]
})
export class WorkerModule { }
