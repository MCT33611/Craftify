import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './pages/profile/profile.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { SecurityComponent } from './components/security/security.component';

const routes: Routes = [
  {
    path:"",
    component:ProfileComponent,
    children:[
      {path:"edit",component:EditProfileComponent},
      {path:"security/:email",component:SecurityComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileRoutingModule { }
