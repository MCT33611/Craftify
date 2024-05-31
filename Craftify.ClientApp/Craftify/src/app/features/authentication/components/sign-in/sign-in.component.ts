import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ILogin } from '../../models/ilogin';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private auth: AuthService,
    private router: Router
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
        complete:()=>{
          this.toastr.success("wecome to proportel")
          this.router.navigate(['/home']);
        },
        error:()=>this.toastr.error("something went wrong")

      });

    } else {
      this.toastr.error("cridentials are not valid")
    }
  }
}
