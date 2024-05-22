declare var google: any;
import { Component, Input, NgZone, OnInit } from '@angular/core';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';
import { Router } from '@angular/router';
import { AuthService } from '../../../modules/auth/services/auth.service';
import { environment } from '@env/environment.prod';

@Component({
  selector: 'app-google-login-button',
  standalone: true,
  imports: [],
  templateUrl: './google-login-button.component.html',
  styleUrl: './google-login-button.component.css'
})
export class GoogleLoginButtonComponent implements OnInit {



  constructor(
    private _ngZone: NgZone,
    private auth: AuthService,
    private router: Router
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

    this.auth.loginWithGoogle(response.credential).subscribe(
      () => {
        this.router.navigate(['/home']);
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
