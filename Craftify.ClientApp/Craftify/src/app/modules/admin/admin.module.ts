import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { WorkerListComponent } from './components/worker-list/worker-list.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MaterialModule } from '../../core/modules/material/material.module';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { ServiceDetailsComponent } from './components/service-details/service-details.component';
import { WorkerDetailsComponent } from './components/worker-details/worker-details.component';
import { CustomerDetailsComponent } from './components/customer-details/customer-details.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AdminLayoutComponent } from './pages/admin-layout/admin-layout.component';
import { TabelWithFilterComponent } from '@ui/tabel-with-filter/tabel-with-filter.component';


@NgModule({
  declarations: [
    CustomerListComponent,
    ServiceListComponent,
    WorkerListComponent,
    DashboardComponent,
    CategoryListComponent,
    ServiceDetailsComponent,
    WorkerDetailsComponent,
    CustomerDetailsComponent,
    CategoryFormComponent,
    AdminLayoutComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MaterialModule,
    RouterOutlet,
    RouterLink,
    TabelWithFilterComponent
  ]
})
export class AdminModule { }
