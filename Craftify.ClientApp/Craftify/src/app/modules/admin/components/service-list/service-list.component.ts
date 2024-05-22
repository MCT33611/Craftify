import { Component, ViewChild, inject } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { IService } from '../../../../core/models/iservice';
import { ServiceManagementService } from '../../services/service-management.service';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrl: './service-list.component.css'
})
export class ServiceListComponent {
  router = inject(Router);
  ELEMENT_DATA : IService[] =[]

  displayedColumns: string[] = ['title', 'zipCode', 'price','availability'];


  constructor(private serviceManagement : ServiceManagementService){
    serviceManagement.getAllServices().subscribe(
      (res:any[])=> this.ELEMENT_DATA = res
    );
  }
}
