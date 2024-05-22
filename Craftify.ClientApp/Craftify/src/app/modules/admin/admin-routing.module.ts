import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { WorkerListComponent } from './components/worker-list/worker-list.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { ServiceDetailsComponent } from './components/service-details/service-details.component';
import { WorkerDetailsComponent } from './components/worker-details/worker-details.component';
import { CustomerDetailsComponent } from './components/customer-details/customer-details.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { AdminLayoutComponent } from './pages/admin-layout/admin-layout.component';

const routes: Routes = [
  {
    path: "",
    component:AdminLayoutComponent,
    children: [
      { path: "dashboard", component: DashboardComponent },
      { path: "service/list", component: ServiceListComponent },
      { path: "worker/list", component: WorkerListComponent },
      { path: "customer/list", component: CustomerListComponent },
      { path: "category/list", component: CategoryListComponent },
      { path: "service/details/:id", component: ServiceDetailsComponent },
      { path: "worker/details/:id", component: WorkerDetailsComponent },
      { path: "customer/details/:id", component: CustomerDetailsComponent },
      { path: "category/create", component: CategoryFormComponent },
      { path: "category/update/:id", component: CategoryFormComponent },
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
