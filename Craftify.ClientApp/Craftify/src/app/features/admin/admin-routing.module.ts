import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './pages/layout/layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  {
    path:"",
    component:LayoutComponent,
    children:[
      {
        path:"",
        redirectTo:'dashboard',
        pathMatch:'full'
      },
      {
        path:"dashboard",
        component:DashboardComponent
      },
      {
        path:"profiles",
        loadChildren:()=>import("./modules/profiles/profiles.module").then(m => m.ProfilesModule)
      },      {
        path:"category",
        loadChildren:()=>import("./modules/category-management/category-management.module").then(m => m.CategoryManagementModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
