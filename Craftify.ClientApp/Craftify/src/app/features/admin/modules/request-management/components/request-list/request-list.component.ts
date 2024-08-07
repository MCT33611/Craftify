import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { RequestService } from '../../services/request.service';
import { AlertService } from '../../../../../../services/alert.service';
import { ColumnConfig, ColumnType } from '../../../../../../shared/components/ui-datatable/column-config.model';
import { IBooking } from '../../../../../../models/ibooking';
import { HttpErrorResponse } from '@angular/common/http';
import { Subject, takeUntil } from 'rxjs';
import { IBookingStatus } from '../../../../../../models/ibooking-status';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css'
})
export class RequestListComponent implements OnInit, OnDestroy {
  requestService = inject(RequestService);
  alertService = inject(AlertService);

  data: {}[] = [];
  columns: ColumnConfig[] = [
    { key: 'status', type: ColumnType.Text, header: 'Status' },
    { key: 'date', type: ColumnType.Text, header: 'Date' },
    { key: 'locationName', type: ColumnType.Text, header: 'Location' },
    { key: 'workingTime', type: ColumnType.Text, header: 'Working Time' },
    { key: 'customerName', type: ColumnType.Text, header: 'Customer' },
    { key: 'providerName', type: ColumnType.Text, header: 'Provider' },
    { key: 'action', type: ColumnType.Action, header: 'Reject' }
  ];

  private destroy$ = new Subject<void>();
  ngOnInit(): void {
    this.requestService.getAllRequest().pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      next: (res: any) => {
        if (res && Array.isArray(res.$values)) {
          this.data = res.$values.map((booking: IBooking) => ({
            ...booking,
            locationName: booking.locationName?.split(',')[5] || 'N/A',
            status: IBookingStatus[booking.status!] || 'Unknown',
            customerName: booking.customer?.firstName || 'N/A',
            providerName: booking.provider?.user?.firstName || 'N/A',
            action: `/admin/request/reject/${booking.id}`
          }));
        } else if (Array.isArray(res)) {
          // If res is already an array, use it directly
          this.data = res.map((booking: IBooking) => ({
            ...booking,
            locationName: booking.locationName?.split(',')[5] || 'N/A',
            status: IBookingStatus[booking.status!] || 'Unknown',
            customerName: booking.customer?.firstName || 'N/A',
            providerName: booking.provider?.user?.firstName || 'N/A',
            action: `/admin/request/reject/${booking.id}`
          }));
        } else {
          console.error('Unexpected response format:', res);
          this.alertService.error('Unexpected response format from server');
        }
      },
      error: (err: HttpErrorResponse) => {
        this.alertService.error(err.message);
      }
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}