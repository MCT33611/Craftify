import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AlertService } from '../../../../services/alert.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  isEmailValid = false;
  forgotForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private alert : AlertService,
    private auth: AuthService,
  ) {

    this.forgotForm = this.fb.group({
      email: [
        '',
        [Validators.required, Validators.email]
      ],
    });
  }

  onEmailSubmit() {

    if (this.forgotForm.valid) {

      this.auth.forgetPassword(this.forgotForm.value.email).subscribe({
        complete:()=>{
          this.isEmailValid = true;
        },
        error:(error:any)=>this.alert.error(`${error.status} : ${error.error.title}`)
      });

    } else {
      this.alert.error("cridentials are not valid")
    }
  }


}
