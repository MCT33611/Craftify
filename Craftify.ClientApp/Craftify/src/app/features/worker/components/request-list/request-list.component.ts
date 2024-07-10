import { Component } from '@angular/core';
import { IBooking } from '../../../../models/ibooking';
import { WorkerService } from '../../services/worker.service';
import { IBookingStatus } from '../../../../models/ibooking-status';
import { Router } from '@angular/router';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrl: './request-list.component.css'
})
export class RequestListComponent {
  bookings: IBooking[] = [];

  constructor(
    private workerService: WorkerService,
    private router : Router
  ) { }

  ngOnInit(): void {
    this.loadBookings();
  }

  getBookingStatus(status: number) {
    return IBookingStatus[status]
  }

  isDatePassed(dateString: string): boolean {
    const bookingDate = new Date(dateString);
    const today = new Date();
    today.setHours(0, 0, 0, 0); // Set to start of day for accurate comparison
    return bookingDate < today;
  }

  cancelBooking(booking : IBooking): void {
    // Implement the logic to cancel a booking
    booking.status = IBookingStatus.Cancelled;
    this.workerService.rescheduleBooking(booking).subscribe({
    });
  }

  loadBookings(): void {
    this.workerService.getAllRequest().subscribe(
      (data: IBooking[]) => {
        this.bookings = data;
      },
      (error) => {
        console.error('Error fetching bookings:', error);
      }
    );
  }

  getGoogleMapsUrl(location: string): string {
    const [lat, lng] = location.split(',');
    return `https://www.google.com/maps?q=${lat},${lng}`;
  }

  acceptBooking(booking:IBooking): void {
    // Implement accept logic
    console.log('Accepting booking:', booking);
    booking.status = IBookingStatus.Accepted;
    this.workerService.rescheduleBooking(booking).subscribe({
    });
  }

  rejectBooking(booking:IBooking): void {
    // Implement reject logic
    console.log('Rejecting booking:', booking);
    booking.status = IBookingStatus.Rejected;
    this.workerService.rescheduleBooking(booking).subscribe({
    });
  }

  startMassage(booking:IBooking){
    this.router.navigate(['/worker/chat', booking.id]);
  }
}
