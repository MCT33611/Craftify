import { Component, ViewChild, inject } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { CategoryManagementService } from '../../services/category-management.service';

export interface PeriodicElement {
  name: string;
  min: number;
  max: number;
}

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent {
  router = inject(Router);
  ELEMENT_DATA : PeriodicElement[] =[]

  displayedColumns: string[] = ['name', 'min', 'max','action'];


  constructor(private categoryService : CategoryManagementService){
    categoryService.getAllCategories().subscribe(
      (res:any[])=> this.ELEMENT_DATA = res
    );
  }
}