import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth.service';

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
    private toastr: ToastrService,
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
        complete: () => {
          this.isEmailValid = true;
        },
        error: (err) => this.toastr.error(err.error, err.status)
      });

    } else {
      this.toastr.error("cridentials are not valid")
    }
  }


}
