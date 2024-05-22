import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { strongPasswordValidator } from '../../validators/strongPasswordValidator';
import { IRegistration } from '../../../../core/models/iregistration';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

export class PasswordErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    return (control && control.dirty && control.hasError('passwordMismatch')) ?? false;
  }
}

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
        [Validators.required,this.noNumbersOrSpecialCharacters]
      ],
      lastName: [
        '',
        [Validators.required,this.noNumbersOrSpecialCharacters]
      ],
      password: [
        '',
        [Validators.required, this.passwordStrengthValidator]
      ],
      confirmPassword: [
        '',
        [Validators.required]
      ]
    }, {
      validators: this.matchPasswords
    });

  }



  matchPasswords(formGroup: FormGroup) {

    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { passwordsDoNotMatch: true };
  }

  passwordStrengthValidator(control: any) {
    const password = control.value;
    const strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\W]).{8,}$/;
    return strongRegex.test(password) ? null : { weakPassword: true };
  }

  noNumbersOrSpecialCharacters(control : any) {
    const regex = /^[a-zA-Z\s]*$/;
    if (!regex.test(control.value)) {
      return { containsNumbersOrSpecialCharacters: true };
    }
    return null;
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
          this.router.navigate(["/home"])
        },
        error: (err:any) => {
          console.error(err);
          this.toastr.error(err.error,err.status);
        }

      })

    } else {
      console.error('Form is not valid');
    }
  }
}
