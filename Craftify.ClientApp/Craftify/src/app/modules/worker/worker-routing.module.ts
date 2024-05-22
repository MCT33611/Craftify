import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { ServiceFormComponent } from './components/service-form/service-form.component';

const routes: Routes = [
  {path:"service/list", component:ServiceListComponent},
  {path:"service/edit/:id", component:ServiceFormComponent},
  {path:"service/create", component:ServiceFormComponent},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WorkerRoutingModule { }
