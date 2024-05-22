import { Component, Input, Type } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MaterialModule } from '../../../core/modules/material/material.module';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';
export interface PeriodicElement {
  name: string;
  min: number;
  max: number;
}

@Component({
  selector: 'app-tabel-with-filter',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './tabel-with-filter.component.html',
  styleUrl: './tabel-with-filter.component.css'
})
export class TabelWithFilterComponent{
  @Input({required:true}) ELEMENT_DATA : any[] = []
  @Input({required:true}) displayedColumns: string[] = [];

  dataSource = new MatTableDataSource(this.ELEMENT_DATA);

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
