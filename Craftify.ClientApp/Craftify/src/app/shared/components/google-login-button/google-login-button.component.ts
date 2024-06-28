declare var google: any;
import { Component, NgZone, OnInit } from '@angular/core';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../../features/authentication/services/auth.service';
import { AlertService } from '../../../services/alert.service';
import { IRoles } from '../../../core/constants/roles';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthResponse } from '../../../models/auth-response';

@Component({
  selector: 'app-google-login-button',
  standalone: true,
  imports: [],
  templateUrl: './google-login-button.component.html',
  styleUrl: './google-login-button.component.css'
})
export class GoogleLoginButtonComponent implements OnInit {
  constructor(
    private _auth: AuthService,
    private _router: Router,
    private _alert: AlertService
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
    
    this._auth.LoginWithGoogle(response.credential).subscribe(
      {
        next: (res: AuthResponse) => {
          if(res.user.role === IRoles.Role_Admin) {
            this._router.navigate(['/admin'])
          }
          else if(res.user.role === IRoles.Role_Customer){
            this._router.navigate(['/home'])

          }

        },

        error: (error: HttpErrorResponse) => {
          console.log(error);
          
          this._alert.error(`${error.status}`)
        }
      }
    );
  }
}