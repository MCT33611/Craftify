import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './pages/layout/layout.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { OtpComponent } from './components/otp/otp.component';

const routes: Routes = [
  {
    path:"",
    component:LayoutComponent,
    children:[
      {
        path:'sign-in',
        component:SignInComponent
      },
      {
        path:'sign-up',
        component:SignUpComponent
      },
      {
        path:'otp/:email',
        component:OtpComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }
