import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WorkerRoutingModule } from './worker-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ContentComponent } from './components/content/content.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { LayoutComponent } from './layout/layout.component';
import { MaterialModule } from '../../shared/material/material.module';
import { RouterOutlet } from '@angular/router';


@NgModule({
  declarations: [
    DashboardComponent,
    ContentComponent,
    SidebarComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    WorkerRoutingModule,
    HeaderComponent,
    FooterComponent,
    MaterialModule,
    RouterOutlet
  ]
})
export class WorkerModule { }
