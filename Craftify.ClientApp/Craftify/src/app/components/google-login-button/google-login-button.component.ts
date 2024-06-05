declare var google: any;
import { Component, NgZone, OnInit } from '@angular/core';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../features/authentication/services/auth.service';
import { AlertService } from '../../services/alert.service';
import { Role_Admin, Role_Customer } from '../../core/constants/roles';

@Component({
  selector: 'app-google-login-button',
  standalone: true,
  imports: [],
  templateUrl: './google-login-button.component.html',
  styleUrl: './google-login-button.component.css'
})
export class GoogleLoginButtonComponent implements OnInit {
  constructor(
    private auth: AuthService,
    private router: Router,
    private alert: AlertService
  ) {

  }
  ngOnInit(): void {

    google.accounts.id.initialize({
      client_id: environment.GoogleClientId,
      callback: this.handleCredentialResponse.bind(this)

    });
    google.accounts.id.renderButton(
      document.getElementById("google-btn"),
      { theme: "filled_blue", size: "large", width: "100%" }
    );

    google.accounts.id.prompt((notification: PromptMomentNotification) => { });
  }
  async handleCredentialResponse(response: CredentialResponse) {
    this.auth.LoginWithGoogle(response.credential).subscribe(
      {
        next: (res: any) => {
          if(res.user.role === Role_Admin) {
            this.router.navigate(['/admin'])
          }
          else if(res.user.role === Role_Customer){
            this.router.navigate(['/home'])

          }

        },

        error: (error: any) => {
          this.alert.error(`${error.status} : ${error.error[0].description}`)
        }
      }
    );
  }
}
