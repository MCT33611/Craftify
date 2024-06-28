import { Component, ElementRef, QueryList, ViewChildren, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AlertService } from '../../../../services/alert.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrl: './otp.component.css'
})
export class OtpComponent implements OnInit, OnDestroy {
  countdown: number = 60;
  isCountdownActive: boolean = false;
  canResendOTP: boolean = false;
  isInValid: boolean = false;
  otpForm: FormGroup;
  email!: string | null;
  @ViewChildren('otpInput') otpInputs !: QueryList<ElementRef>;

  private destroy$ = new Subject<void>();
  private countdownInterval: any;

  constructor(
    private _fb: FormBuilder,
    private _auth: AuthService,
    private _router: Router,
    private _route: ActivatedRoute,
    private _alert: AlertService
  ) {
    this.otpForm = this._fb.group({
      otp1: ['', [Validators.required, Validators.maxLength(1)]],
      otp2: ['', [Validators.required, Validators.maxLength(1)]],
      otp3: ['', [Validators.required, Validators.maxLength(1)]],
      otp4: ['', [Validators.required, Validators.maxLength(1)]],
    });
  }

  ngOnInit(): void {
    this.startCountdown();
    this._route.paramMap.pipe(
      takeUntil(this.destroy$)
    ).subscribe(params => {
      this.email = params.get('email');
      if (this.email) {
        this.sendOtp();
      } else {
        this._alert.error("OTP sender: email is undefined!");
        this._router.navigate(['/auth/sign-up']);
      }
    });
  }

  sendOtp() {
    this._auth.sentOtp(this.email!).pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      complete: () => this._alert.success("Please check your mailbox"),
      error: (error: HttpErrorResponse) => {
        this._alert.error(`${error.status}: ${error.error.title}`);
        this._router.navigate(['/auth/sign-up']);
      }
    });
  }

  onInputChange(event: KeyboardEvent, index: number) {
    const target = event.target as HTMLInputElement;
    const value = target.value;

    if (value && index < this.otpInputs.length - 1) {
      this.otpInputs.toArray()[index + 1].nativeElement.focus();
    }

    if (!value && index > 0 && event.key === 'Backspace') {
      this.otpInputs.toArray()[index - 1].nativeElement.focus();
    }
    this.isInValid = false;
  }

  onSubmit() {
    if (this.otpForm.valid) {
      const otp = Object.values(this.otpForm.value).join('');
      this._auth.confirmEmail(otp, this.email!).pipe(
        takeUntil(this.destroy$)
      ).subscribe({
        complete: () => {
          this._router.navigate(['/auth/sign-in']);
        },
        error: (error: HttpErrorResponse) => this._alert.error(`${error.status}: ${error.error.title}`)
      });
    }
  }

  startCountdown() {
    this.countdown = 60;
    this.isCountdownActive = true;
    this.canResendOTP = false;

    this.clearCountdownInterval();
    this.countdownInterval = setInterval(() => {
      if (this.countdown > 0) {
        this.countdown--;
      } else {
        this.clearCountdownInterval();
        this.isCountdownActive = false;
        this.canResendOTP = true;
      }
    }, 1000);
  }

  clearCountdownInterval() {
    if (this.countdownInterval) {
      clearInterval(this.countdownInterval);
    }
  }

  resendOTP() {
    if (this.canResendOTP) {
      this.startCountdown();
      this.sendOtp();
    }
  }

  containsCharacters(otpValue: string): boolean {
    const characterRegex = /[a-zA-Z]/;
    return characterRegex.test(otpValue);
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
    this.clearCountdownInterval();
  }
}