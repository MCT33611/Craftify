import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ILogin } from '../../models/ilogin';
import { AlertService } from '../../../../services/alert.service';
import { ProfileService } from '../../../profile/services/profile.service';
import { ProfileStore } from '../../../../shared/store/profile.store';
import { Role_Admin, Role_Customer } from '../../../../core/constants/roles';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  loginForm!: FormGroup;
  profileStore = inject(ProfileStore);
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private alert: AlertService,
    private profile: ProfileService,

  ) {

    this.loginForm = this.fb.group({
      email: [
        '',
        [Validators.required, Validators.email]
      ],
      password: [
        '',
        Validators.required
      ]
    });
  }




  onSubmit() {

    if (this.loginForm.valid) {
      let user: ILogin = {
        email: this.loginForm.value.email,
        password: this.loginForm.value.password
      }

      this.auth.login(user).subscribe({
        next: (res: any) => {
          if (res.user.role === Role_Admin) {
            this.router.navigate(['/admin'])
          }
          else if (res.user.role === Role_Customer) {
            this.router.navigate(['/home'])

          }

        },
        error: (err) => this.alert.error(`${err.status} : invalid creditials`)


      });

    } else {
      this.alert.warning("form is not valid")
    }
  }
}
