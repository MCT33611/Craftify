import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { IRegistration } from '../../models/iregistration';
import { matchPasswords } from '../../../../shared/utils/matchPasswordsValidator'
import { noNumbersOrSpecialCharacters } from '../../../../shared/utils/noNumbersOrSpecialCharacters';
import { passwordStrengthValidator } from '../../../../shared/utils/passwordStrengthValidator';
import { AlertService } from '../../../../services/alert.service';
@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  registrationForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private alert: AlertService,
    private router: Router) {
    this.registrationForm = this.fb.group({
      email: [
        '',
        [Validators.required, Validators.email]
      ],
      firstName: [
        '',
        [Validators.required, noNumbersOrSpecialCharacters]
      ],
      lastName: [
        '',
        [Validators.required, noNumbersOrSpecialCharacters]
      ],
      password: [
        '',
        [Validators.required, passwordStrengthValidator]
      ],
      confirmPassword: [
        '',
        [Validators.required]
      ]
    }, {
      validators: matchPasswords
    });

  }

  onSubmit() {
    if (this.registrationForm.valid) {
      let user: IRegistration = {
        email: this.registrationForm.value.email,
        firstName: this.registrationForm.value.firstName,
        lastName: this.registrationForm.value.lastName,
        Password: this.registrationForm.value.password,
      }
      this.auth.register(user).subscribe({
        complete: () => {
          this.router.navigate([`/auth/otp/${user.email}`])
        },
        error: (error:any) => {
          this.alert.error(`${error.status} : ${error.error.title}`)
        }

      })

    } else {
      this.alert.warning('Form is not valid');
    }
  }
}
