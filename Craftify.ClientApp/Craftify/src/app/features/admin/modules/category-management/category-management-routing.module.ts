import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './components/list/list.component';
import { EditComponent } from './components/edit/edit.component';
import { CreateComponent } from './components/create/create.component';

const routes: Routes = [
  {
    path:"",
    pathMatch:'full',
    redirectTo:"list"
  },
  {
    path:"list",
    component:ListComponent
  },
  {
    path:"edit/:id",
    component:EditComponent
  },
  {
    path:"create",
    component:CreateComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryManagementRoutingModule { }
