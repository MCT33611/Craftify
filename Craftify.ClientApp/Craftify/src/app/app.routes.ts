import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
    {
        path:'home',
        component:HomeComponent
    },
    {
        path:"auth",
        loadChildren:()=>import("../app/features/authentication/authentication.module").then(m => m.AuthenticationModule)
    }

];
