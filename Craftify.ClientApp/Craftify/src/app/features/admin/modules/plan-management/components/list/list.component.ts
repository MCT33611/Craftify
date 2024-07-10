import { Component, OnInit, OnDestroy } from '@angular/core';
import { ColumnConfig, ColumnType } from '../../../../../../shared/components/ui-datatable/column-config.model';
import { PlanService } from '../../services/plan.service';
import { IPlan } from '../../../../../../models/iplan';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent implements OnInit, OnDestroy {

  data: IPlan[] = [];
  private destroy$ = new Subject<void>();

  constructor(private _planService: PlanService) {}

  columns: ColumnConfig[] = [
    { key: 'title', type: ColumnType.Text, header: 'Title' },
    { key: 'price', type: ColumnType.Text, header: 'Min Price' },
    { key: 'duration', type: ColumnType.Text, header: 'Max Price' },
    { key: 'details', type: ColumnType.Action, header: 'Edit' },
    { key: 'delete', type: ColumnType.Action, header: 'Delete' }
  ];

  ngOnInit(): void {
    this._planService.getAll().pipe(
      takeUntil(this.destroy$)
    ).subscribe((res: IPlan[]) => {
      this.data = res.map((data: IPlan) => ({
        ...data,
        details: `/admin/plan/edit/${data.id}`,
        delete: `/admin/plan/delete/${data.id}`
      }));
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}