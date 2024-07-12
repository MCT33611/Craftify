import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { IBooking } from '../../../../models/ibooking';
import { CustomerService } from '../../services/customer.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IBookingStatus } from '../../../../models/ibooking-status';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../../../../services/alert.service';
import { MapDialogComponent } from '../../../../shared/components/map/map-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';

interface Location {
  lat: number;
  lng: number;
}

interface LocationResponse {
  display_name: string;
}

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css',
})
export class RequestListComponent implements OnInit, OnDestroy {
  bookings: IBooking[] = [];
  showRescheduleForm = false;
  rescheduleForm: FormGroup;
  selectedBooking: IBooking | null = null;
  minDate = new Date();
  isLocationLoading = false;
  currentLocation: Location | null = null;
  private dialog = inject(MatDialog);
  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private http: HttpClient,
    private alert: AlertService
  ) {
    this.rescheduleForm = this.fb.group({
      workingTime: ['', [Validators.required, Validators.min(1)]],
      date: ['', Validators.required],
      locationName: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.customerService.getAllRequest().pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      next: (bookings: IBooking[]) => {
        this.bookings = bookings;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error loading bookings:', error);
      }
    });
  }

  openMessageDialog(booking: IBooking): void {
    console.log('Open message dialog for booking:', booking);
  }

  getColorStatus(status: IBookingStatus): string {
    switch (status) {
      case IBookingStatus.Pending: return 'bg-yellow-200';
      case IBookingStatus.Cancelled: return 'bg-red-200';
      case IBookingStatus.Completed: return 'bg-green-200';
      case IBookingStatus.Accepted: return 'bg-blue-200';
      default: return "bg-white";
    }
  }

  getStatus(status: IBookingStatus): string {
    return IBookingStatus[status];
  }

  openRescheduleDialog(booking: IBooking): void {
    this.selectedBooking = booking;
    this.rescheduleForm.patchValue({
      workingTime: booking.workingTime,
      date: new Date(booking.date),
      locationName: booking.locationName
    });
    this.showRescheduleForm = true;
  }

  onCancel(booking: IBooking): void {
    const updatedBooking: IBooking = {
      ...booking,
      status: IBookingStatus.Cancelled
    };
    this.customerService.rescheduleBooking(updatedBooking).pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      next: () => {
        this.showRescheduleForm = false;
        this.loadBookings();
      },
      error: (error) => {
        console.error('Error rescheduling booking', error);
      }
    });
  }

  onRescheduleSubmit(): void {
    if (this.rescheduleForm.valid && this.selectedBooking) {
      const updatedBooking: IBooking = {
        ...this.selectedBooking,
        ...this.rescheduleForm.value
      };
      this.customerService.rescheduleBooking(updatedBooking).pipe(
        takeUntil(this.destroy$)
      ).subscribe({
        next: () => {
          this.showRescheduleForm = false;
          this.loadBookings();
        },
        error: (error) => {
          console.error('Error rescheduling booking', error);
        }
      });
    }
  }

  setLocation(lat: number, lng: number): void {
    this.rescheduleForm.patchValue({ location: `${lat},${lng}` });
    this.getLocationName(lat, lng);
  }

  getLocationName(lat: number, lng: number): void {
    this.http.get<LocationResponse>(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}`).pipe(
      takeUntil(this.destroy$)
    ).subscribe({
      next: (result) => {
        this.rescheduleForm.patchValue({ locationName: result.display_name });
        this.isLocationLoading = false;
      },
      error: () => {
        this.alert.error('Failed to get location name. Please enter manually.');
        this.isLocationLoading = false;
      }
    });
  }

  getCurrentLocation(): void {
    this.isLocationLoading = true;
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.currentLocation = {
            lat: position.coords.latitude,
            lng: position.coords.longitude
          };
          this.setLocation(this.currentLocation.lat, this.currentLocation.lng);
        },
        () => {
          this.alert.error('Unable to get current location. Please enter manually.');
          this.isLocationLoading = false;
        }
      );
    } else {
      this.alert.error('Geolocation is not supported by this browser. Please enter location manually.');
      this.isLocationLoading = false;
    }
  }

  openMapDialog(): void {
    const dialogRef = this.dialog.open(MapDialogComponent, {
      width: '80vw',
      height: '80vh',
      data: { currentLocation: this.currentLocation, locationName: this.rescheduleForm.get('locationName')?.value }
    });

    dialogRef.afterClosed().pipe(
      takeUntil(this.destroy$)
    ).subscribe(result => {
      if (result) {
        this.setLocation(result.lat, result.lng);
      }
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}