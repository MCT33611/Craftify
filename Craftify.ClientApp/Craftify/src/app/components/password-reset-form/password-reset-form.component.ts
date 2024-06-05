import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../features/authentication/services/auth.service';
import { Router } from '@angular/router';
import { passwordStrengthValidator } from '../../shared/utils/passwordStrengthValidator';
import { matchPasswords } from '../../shared/utils/matchPasswordsValidator';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'app-password-reset-form',
  standalone:true,
  imports:[
    ReactiveFormsModule,
  ],
  templateUrl: './password-reset-form.component.html',
  styleUrl: './password-reset-form.component.css'
})
export class PasswordResetFormComponent {
  @Input({required:true}) email!:string;
  @Input() redirectTo : string = '/auth/sign-in';
  resetForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private alert: AlertService
  ){
    this.resetForm = this.fb.group({
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



  onPasswordSubmit(){
    if (this.resetForm.valid) {

      this.auth.resetPassword(this.email,this.resetForm.value.password).subscribe({
        complete:()=>{
          this.alert.success("password changed successfully")
          if(this.redirectTo)
            this.router.navigate([this.redirectTo]);
        },
        error:(error:any)=>{
          this.alert.error(`${error.status} : ${error.error.title}`)
          
        }
      });

    } else {
      this.alert.warning("cridentials are not valid")
    }
  }

}
