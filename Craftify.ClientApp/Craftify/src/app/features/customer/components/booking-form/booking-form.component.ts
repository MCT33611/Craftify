import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { IWorker } from '../../../../models/iworker';
import { TokenService } from '../../../../services/token.service';
import { AlertService } from '../../../../services/alert.service';
import { CustomerService } from '../../services/customer.service';
import { MapDialogComponent } from '../../../../shared/components/map/map-dialog.component';
import { HttpClient } from '@angular/common/http';
import { IBookingStatus } from '../../../../models/ibooking-status';
import { Router } from '@angular/router';

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  @Input() provider!: IWorker;
  @Output() close = new EventEmitter();
  bookingForm!: FormGroup;
  currentLocation: any;
  isLocationLoading: boolean = true;
  minDate: Date = new Date(); // Set minimum date to today

  private fb = inject(FormBuilder);
  private tokenService = inject(TokenService);
  private alertService = inject(AlertService);
  private customerService = inject(CustomerService);
  private dialog = inject(MatDialog);
  private http = inject(HttpClient);
  private _router = inject(Router);
  dialogRef !:MatDialogRef<MapDialogComponent,any>;
  ngOnInit() {
    this.initForm();
    this.getCurrentLocation();
  }

  initForm() {
    this.bookingForm = this.fb.group({
      workingTime: ['', [Validators.required, Validators.min(1)]],
      date: ['', Validators.required],
      location: ['', Validators.required],
      locationName: ['', Validators.required],
      customerId: [this.tokenService.getUserId(), Validators.required],
      providerId: [this.provider.id, Validators.required],
      status: [IBookingStatus.Pending] // Assuming 'Pending' is the initial status
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
          this.alertService.error('Unable to get current location. Please enter manually.');
          this.isLocationLoading = false;
        }
      );
    } else {
      this.alertService.error('Geolocation is not supported by this browser. Please enter location manually.');
      this.isLocationLoading = false;
    }
  }

  setLocation(lat: number, lng: number) {
    this.bookingForm.patchValue({ location: `${lat},${lng}` });
    this.getLocationName(lat, lng);
  }

  getLocationName(lat: number, lng: number) {
    this.http.get(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}`)
      .subscribe((result: any) => {
        this.bookingForm.patchValue({ locationName: result.display_name });
        this.isLocationLoading = false;
      }, error => {
        this.alertService.error('Failed to get location name. Please enter manually.');
        this.isLocationLoading = false;
      });
  }

  openMapDialog() {
    this.dialogRef = this.dialog.open(MapDialogComponent, {
      width: '80vw',
      height: '80vh',
      data: { currentLocation: this.currentLocation, locationName: this.bookingForm.get('locationName')?.value }
    });

    this.dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.setLocation(result.lat, result.lng);
      }
    });
    
  }

  onSubmit() {
    if (this.bookingForm.valid) {
      const bookingData = this.bookingForm.value;

      // Format the date as a string (assuming you want ISO format)
      bookingData.date = new Date(bookingData.date).toISOString().split('T')[0];

      // bookedAt will be set by the server, so we don't include it here

      this.customerService.book(bookingData).subscribe({
        next: (response) => {
          this.alertService.success('Booking created successfully!');
          this._router.navigate(['/customer/services']);
          // Handle successful booking (e.g., navigate to confirmation page)
        },
        error: (error) => {
          this.alertService.error('Failed to create booking. Please try again.');
        }
      });
    }
  }
}