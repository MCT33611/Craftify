import { Routes } from '@angular/router';
import { HomeComponent } from '@pages/home/home.component';
import { LandingComponent } from '@pages/landing/landing.component';

export const routes: Routes = [
    { path: '', redirectTo: '/landing', pathMatch: 'full' },

    { path: 'landing', component: LandingComponent },

    { path: 'home', component: HomeComponent },

    {path: 'auth', loadChildren:()=>import('../app/modules/auth/auth.module').then(m => m.AuthModule)},
    {path: 'profile', loadChildren:()=>import('../app/modules/profile/profile.module').then(m => m.ProfileModule)},
    {path: 'admin', loadChildren:()=>import('../app/modules/admin/admin.module').then(m => m.AdminModule)},
    {path: 'worker', loadChildren:()=>import('../app/modules/worker/worker.module').then(m => m.WorkerModule)}
];
