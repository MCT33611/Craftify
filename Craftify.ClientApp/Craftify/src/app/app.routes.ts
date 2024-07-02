import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { authGuard } from './core/guards/auth.guard';
import { loggedInGuard } from './core/guards/logged-in.guard';
import { roleGuard } from './core/guards/role.guard';
import { IRoles } from './core/constants/roles';

export const routes: Routes = [
    {
        path:'',
        redirectTo:'home',
        pathMatch:'full',
    },
    {
        path:'home',
        component:HomeComponent,
        canActivate: [authGuard] ,
    },
    {
        path:"auth",
        canActivate: [loggedInGuard] ,
        loadChildren:()=>import("../app/features/authentication/authentication.module").then(m => m.AuthenticationModule)
    },
    {
        path:"profile",
        canActivate: [authGuard] ,
        loadChildren:()=>import("../app/features/profile/profile.module").then(m => m.ProfileModule)
    },
    {
        path:"admin",
        canActivate: [authGuard,roleGuard] ,
        data:{role:IRoles.Role_Admin},
        loadChildren:()=>import("../app/features/admin/admin.module").then(m => m.AdminModule)
    },
    {
        path:"worker",
        canActivate: [authGuard,roleGuard] ,
        data:{role:IRoles.Role_Worker},
        loadChildren:()=>import("../app/features/worker/worker.module").then(m => m.WorkerModule)
    },
    {
        path:"booking-flow",
        canActivate: [authGuard,roleGuard] ,
        data:{role:IRoles.Role_Customer},
        loadChildren:()=>import("../app/features/booking-flow/booking-flow.module").then(m => m.BookingFlowModule)
    },


];
