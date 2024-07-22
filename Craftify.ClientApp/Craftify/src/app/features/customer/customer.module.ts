import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RouterLink, RouterOutlet } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { SidePopupComponent } from './components/side-popup/side-popup.component';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { MaterialModule } from '../../shared/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ServiceCardTwoComponent } from '../../shared/components/service-card-two/service-card-two.component';
import { ServiceDetailsComponent } from './components/service-details/service-details.component';
import { ServiceCardComponent } from '../../shared/components/service-card/service-card.component';
import { BookingFormComponent } from './components/booking-form/booking-form.component';
import { RequestListComponent } from './components/request-list/request-list.component';

@NgModule({
  declarations: [
    LayoutComponent,
    SidebarComponent,
    DashboardComponent,
    SidePopupComponent,
    ServiceListComponent,
    ServiceDetailsComponent,
    BookingFormComponent,
    RequestListComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    RouterOutlet,
    RouterLink,
    IonicModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    ServiceCardTwoComponent,
    ServiceCardComponent
  ]
})
export class CustomerModule { }
