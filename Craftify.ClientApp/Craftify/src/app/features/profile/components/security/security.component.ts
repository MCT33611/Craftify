import { Component, inject } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { AlertService } from '../../../../services/alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../authentication/services/auth.service';
import { TokenService } from '../../../../services/token.service';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrl: './security.component.css'
})
export class SecurityComponent {

  profile = inject(ProfileService);
  alert = inject(AlertService);
  router = inject(Router);
  authService = inject(AuthService);
  tokenService = inject(TokenService)

  isEmailValid = false;
  email!: string | null;
  ngOnInit(): void {
    this.email = this.tokenService.getUserEmail();
    if (this.email) {
      this.authService.forgetPassword(this.email).subscribe({
        complete: () => {
          this.isEmailValid = true;
        },
        error: (error) => this.alert.error(`${error.status} : ${error.error[0].description}`)
      });
    }else{
      this.alert.error("password reset init : email is undifined !!");
    }

  }

  delete() {
    if (this.alert.confirm("Are You Sure", "do you want to delete this accout?")) {
      this.profile.delete(this.tokenService.getUserId() ?? 'null').subscribe({
        complete: () => {
          this.alert.success("User Delete successfully");
          this.authService.logout();
          this.router.navigate(['/auth/sign-in'])
        }
      });
    }

  }
}
