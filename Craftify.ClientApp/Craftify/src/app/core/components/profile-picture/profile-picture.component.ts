import { Component, inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { ProfileService } from '../../../modules/auth/services/profile.service';
import { IUser } from '../../models/iuser';

@Component({
  selector: 'app-profile-picture',
  standalone:true,
  imports:[
    CommonModule
  ],
  templateUrl: './profile-picture.component.html',
  styleUrl: './profile-picture.component.css'
})
export class ProfilePictureComponent {
  profile = inject(ProfileService);
  toastr = inject(ToastrService)
  user!: IUser;
  constructor() {
    this.profile.getUser().subscribe({
      next: (res: IUser) => {
        this.user = res
      }
    })
  }
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.profile.uploadProfilePicture( file).subscribe(
        () => {
          this.toastr.success("pocture updated");
          this.profile.getUser().subscribe({
            next: (res: IUser) => {
              this.user = res
            }
          });
        },
        (err) => {
          this.toastr.error(err.error,err.status)
        }
      );
    }
  }
}
