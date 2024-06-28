import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookingFlowRoutingModule } from './booking-flow-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { RouterOutlet } from '@angular/router';
import { WorkersListComponent } from './components/workers-list/workers-list.component';


@NgModule({
  declarations: [
    LayoutComponent,
    WorkersListComponent
  ],
  imports: [
    CommonModule,
    BookingFlowRoutingModule,
    HeaderComponent,
    FooterComponent,
    RouterOutlet
  ]
})
export class BookingFlowModule { }
