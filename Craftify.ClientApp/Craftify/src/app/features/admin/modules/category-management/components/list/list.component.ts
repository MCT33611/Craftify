import { Component, OnInit } from '@angular/core';
import { ICategory } from '../../../../../../models/icategory';
import { CategoryService } from '../../services/category.service';
import { ColumnConfig, ColumnType } from '../../../../../../shared/ui/ui-datatable/column-config.model';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent implements OnInit {

  data: ICategory[] = [];
  constructor(private cateService: CategoryService) {
  }
  columns: ColumnConfig[] = [
    { key: 'picture', type: ColumnType.Image, header: 'Image' },
    { key: 'categoryName', type: ColumnType.Text, header: 'Category Name' },
    { key: 'minmumPrice', type: ColumnType.Text, header: 'Min Price' },
    { key: 'maximumPrice', type: ColumnType.Text, header: 'Max Price' },
    { key: 'details', type: ColumnType.Action, header: 'Details' }
  ];
  ngOnInit(): void {
    this.cateService.getAll().subscribe((res : any) => {
      this.data = res.map((data: any) => ({
        ...data,
        details: `/admin/category/edit/${data.id}`
      }))
    });
  }
}
