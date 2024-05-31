import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { IRegistration } from '../../models/iregistration';
import { matchPasswords } from '../../../../shared/utils/matchPasswordsValidator'
import { noNumbersOrSpecialCharacters } from '../../../../shared/utils/noNumbersOrSpecialCharacters';
import { passwordStrengthValidator } from '../../../../shared/utils/passwordStrengthValidator';
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
    private toastr: ToastrService,
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
        error: err => {
          console.error(err);
          this.toastr.error(err.error, err.status);
        }

      })

    } else {
      console.error('Form is not valid');
    }
  }
}
