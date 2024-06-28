import { Component, inject } from '@angular/core';
import { BookingService } from '../../services/booking.service';

@Component({
  selector: 'app-workers-list',
  templateUrl: './workers-list.component.html',
  styleUrl: './workers-list.component.css'
})
export class WorkersListComponent {
  bookingService = inject(BookingService);
  constructor(){
    this.bookingService.getAllWorkers().subscribe(res => console.log(res));
  }
}
