import { Component, CUSTOM_ELEMENTS_SCHEMA, inject } from '@angular/core';
import { IBooking } from '../../../../models/ibooking';
import { CustomerService } from '../../services/customer.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IBookingStatus } from '../../../../models/ibooking-status';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../../../../services/alert.service';
import { MapDialogComponent } from '../../../../shared/components/map/map-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css',
})
export class RequestListComponent {
  bookings: IBooking[] = [];
  showRescheduleForm = false;
  rescheduleForm!: FormGroup;
  selectedBooking: any;
  minDate = new Date();
  isLocationLoading = false;
  currentLocation!: { lat: number, lng: number };
  private dialog = inject(MatDialog);

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
    this.customerService.getAllRequest().subscribe(
      (bookings: IBooking[]) => {
        this.bookings = bookings;
      },
      (error: HttpErrorResponse) => {
        console.error('Error loading bookings:', error);
      }
    );
  }

  openMessageDialog(booking: IBooking): void {
    // Implement message functionality here
    console.log('Open message dialog for booking:', booking);
  }

  getColorStatus(status: IBookingStatus) {
    switch (status) {
      case 0: return 'bg-yellow-200';
      case 1: return 'bg-red-200';
      case 2: return 'bg-green-200';
      case 3: return 'bg-blue-200';
      default: return "bg-white";
    }
  }
  getStatus(status: IBookingStatus) {
    return IBookingStatus[status]
  }

  openRescheduleDialog(booking: IBooking) {
    this.selectedBooking = booking;
    this.rescheduleForm.patchValue({
      workingTime: booking.workingTime,
      date: new Date(booking.date),
      locationName: booking.locationName
    });
    this.showRescheduleForm = true;
  }

  onCancel(booking: IBooking) {
    const updatedBooking = {
      ...booking,
      status: IBookingStatus.Cancelled
    };
    this.customerService.rescheduleBooking(updatedBooking).subscribe(
      response => {
        this.showRescheduleForm = false;
        this.loadBookings();
      },
      error => {
        console.error('Error rescheduling booking', error);
      }
    );
  }


  onRescheduleSubmit() {
    if (this.rescheduleForm.valid) {
      const updatedBooking = {
        ...this.selectedBooking,
        ...this.rescheduleForm.value
      };
      this.customerService.rescheduleBooking(updatedBooking).subscribe(
        response => {
          // Handle successful reschedule
          this.showRescheduleForm = false;
          this.loadBookings(); // Refresh the booking list
        },
        error => {
          // Handle error
          console.error('Error rescheduling booking', error);
        }
      );
    }
  }

  setLocation(lat: number, lng: number) {
    this.rescheduleForm.patchValue({ location: `${lat},${lng}` });
    this.getLocationName(lat, lng);
  }

  getLocationName(lat: number, lng: number) {
    this.http.get(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}`)
      .subscribe((result: any) => {
        this.rescheduleForm.patchValue({ locationName: result.display_name });
        this.isLocationLoading = false;
      }, error => {
        this.alert.error('Failed to get location name. Please enter manually.');
        this.isLocationLoading = false;
      });
  }

  getCurrentLocation() {
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
        (error) => {
          this.alert.error('Unable to get current location. Please enter manually.');
          this.isLocationLoading = false;
        }
      );
    } else {
      this.alert.error('Geolocation is not supported by this browser. Please enter location manually.');
      this.isLocationLoading = false;
    }
  }

  openMapDialog() {
    const dialogRef = this.dialog.open(MapDialogComponent, {
      width: '80vw',
      height: '80vh',
      data: { currentLocation: this.currentLocation, locationName: this.rescheduleForm.get('locationName')?.value }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.setLocation(result.lat, result.lng);
      }
    });
  }
}


