import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../../../auth/services/profile.service';
import { ToastrService } from 'ngx-toastr';
import { IUser } from '../../../../core/models/iuser';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  profile = inject(ProfileService);
  toastr = inject(ToastrService);
  router = inject(Router);
  user!: IUser;
  constructor() {
    this.profile.getUser().subscribe({
      next: (res: IUser) => {
        this.user = res
      }
    })

  }
  logout() {
    this.profile.logout();
    this.router.navigate(['/login']);
  }
}