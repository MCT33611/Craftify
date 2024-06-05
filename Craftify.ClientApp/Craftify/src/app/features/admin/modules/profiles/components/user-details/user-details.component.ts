import { Component, OnInit, inject } from '@angular/core';
import { UserService } from '../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '../../../../../../services/alert.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent implements OnInit {
  userService = inject(UserService)
  route = inject(ActivatedRoute)
  alert = inject(AlertService)
  userId!: string | null;
  dataSource!: any;


  labels: string[] = [
    'profilePicture',
    'firstName',
    'lastName',
    'email',
    'emailConfirmed',
    'streetAddress',
    'city',
    'state',
    'postalCode',
    'role',
  ];
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = params.get('id'); // Using paramMap instead of queryParams
      if (this.userId)
        this.userService.get(this.userId).subscribe({
          next: (res) => {
            this.dataSource = res;
          },
          error: (error: any) => this.alert.error(error)
        }

        )
      else {

        this.alert.error("userId is not valid , Id : " + this.userId)
      }
    });
  }
}
