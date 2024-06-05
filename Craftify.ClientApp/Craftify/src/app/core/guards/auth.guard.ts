import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../features/authentication/services/auth.service';
import { inject } from '@angular/core';
import { ProfileService } from '../../features/profile/services/profile.service';
import { IUser } from '../../models/iuser';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const profileService = inject(ProfileService)
  const router = inject(Router);

  
  if (authService.isLoggedIn()) {
    profileService.get().subscribe((res : IUser)=>{
      if(!res.emailConfirmed){
        
        router.navigate([`/auth/otp/${res.email}`])
      }
    })
    return true;
  } else {
    router.navigate(['/auth/sign-in'], { queryParams: { returnUrl: state.url } });
    return false;
  }
};
