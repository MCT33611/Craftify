import { Component, inject } from '@angular/core';
import { ProfileStore } from '../../../../shared/store/profile.store';
import { TokenService } from '../../../../services/token.service';
import { ColumnConfig, ColumnType } from '../../../../shared/ui/ui-datatable/column-config.model';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  profileStore = inject(ProfileStore)
  tokenService = inject(TokenService)
  ngOnInit(): void {
    this.profileStore.loadAll();
  }
  columns: ColumnConfig[] = [
    { key: 'profilePicture', type: ColumnType.Image, header: 'Profile Picture' },
    { key: 'name', type: ColumnType.Text, header: 'Name' },
    { key: 'details', type: ColumnType.Action, header: 'Details' }
  ];

  data = [
    { name: 'John Doe', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/1' },
    { name: 'Jane Smith', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/2' },
    { name: 'Alice Johnson', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/3' },
    { name: 'John Doe', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/1' },
    { name: 'Jane Smith', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/2' },
    { name: 'Alice Johnson', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/3' },
    { name: 'John Doe', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/1' },
    { name: 'Jane Smith', profilePicture: 'https://s3.amazonaws.com/images.wealthyaffiliate.com/uploads/1441732/sitecontent/7a021dbfc309db7dbec17b5ce656cf041533880338_cropped.jpg?1533880350', details: '/details/2' },
  ];

  handleFormSubmit(data: any) {
    console.log('Form submitted:', data);
  }
}
