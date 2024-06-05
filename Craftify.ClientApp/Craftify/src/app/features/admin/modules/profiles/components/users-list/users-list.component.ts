import { Component, OnInit, inject } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AlertService } from '../../../../../../services/alert.service';
import { ColumnConfig, ColumnType } from '../../../../../../shared/ui/ui-datatable/column-config.model';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent implements OnInit{
  userService = inject(UserService);
  alertService = inject(AlertService);

  data:any[] = [];
  columns: ColumnConfig[] = [
    { key: 'profilePicture', type: ColumnType.Image, header: 'Profile Picture' },
    { key: 'firstName', type: ColumnType.Text, header: 'First Name' },
    { key: 'email', type: ColumnType.Text, header: 'Email' },
    { key: 'state', type: ColumnType.Text, header: 'State' },
    { key: 'postalCode', type: ColumnType.Text, header: 'Postal Code' },
    { key: 'role', type: ColumnType.Text, header: 'Role' },
    { key: 'details', type: ColumnType.Action, header: 'Details' }
  ];

  ngOnInit(): void {
    this.userService.getAll().subscribe(
      {
        next:(res:any[])=>{
          this.data = res.map((ele:any)=>({
            ...ele,
            details: '../details/'+ele.id
          }))
        },
        error:(err:any)=>{
          this.alertService.error(err);
        }
      }
    );
  }
}
