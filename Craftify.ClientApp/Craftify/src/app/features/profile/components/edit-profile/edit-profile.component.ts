import { Component, inject } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { Router } from '@angular/router';
import { ProfileStore } from '../../../../shared/store/profile.store';
import { IUser } from '../../../../models/iuser';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../../../../services/alert.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {
  profile = inject(ProfileService);
  router = inject(Router);
  alert = inject(AlertService)
  profileStore = inject(ProfileStore);
  fb = inject(FormBuilder);

  user!: IUser;
  profileForm!: FormGroup;

  constructor() {
    this.profileForm = this.fb.group({
      firstName: ["", Validators.required],
      lastName: [""],
      postalCode: [""],
      state: [""],
      city: [""]
    });
  }


  ngOnInit(): void {
    if (this.profile) {
      this.profile.get().subscribe({
        next: (res: IUser) => {
          this.user = res;
          this.initializeForm();
        }
      });
    }
  }
  initializeForm(): void {
    this.profileForm = this.fb.group({
      firstName: [this.user.firstName, Validators.required],
      lastName: [this.user.lastName],
      postalCode: [this.user.postalCode],
      state: [this.user.state],
      city: [this.user.city]
    });
  }
  onSubmit(): void {
    if (this.profileForm.valid) {
      const updatedUserData: IUser = this.profileForm.value;

      this.profile.update(updatedUserData).subscribe({
        complete: () => {
          this.profileStore.loadAll();
          this.alert.success('Profile updated successfully');
        },
        error: (error: any) => {
          console.log(error);
          
          this.alert.error(`${error.status} : ${error.message}`)
        }
      });
    } else {
      this.alert.error('Invalid form data');
    }
  }
}
