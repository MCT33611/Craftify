import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUser } from '../../../../core/models/iuser';
import { ProfileService } from '../../../auth/services/profile.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent {
  user!: IUser;
  profileForm!: FormGroup;

  constructor(
    private profile: ProfileService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
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
      this.profile.getUser().subscribe({
        next: (res: IUser) => {
          this.user = res;
          this.initializeForm();
        },
        error:(err:any)=>this.toastr.error(err.status)  
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
      const updatedUserData : IUser = this.profileForm.value;
      console.log(updatedUserData);
      
      this.profile.update(updatedUserData).subscribe({
        complete: () => {
          this.toastr.success('Profile updated successfully');
        },
        error: (err: any) => {
          this.toastr.error(err.error,err.status);
        }
      });
    } else {
      this.toastr.error('Invalid form data');
    }
  }

}
