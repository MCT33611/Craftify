import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { IWorker } from '../../../models/iworker';
import { environment } from '../../../../environments/environment';
import { handleError } from '../../../shared/utils/handleError';
import { IBooking } from '../../../models/ibooking';
import { TokenService } from '../../../services/token.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private _http: HttpClient, private token: TokenService) { }

  getAllWorkers(): Observable<IWorker[]> {
    return this._http.get<IWorker[]>(`${environment.API_BASE_URL}/Profile/Workers`)
      .pipe(catchError(handleError));
  }
  getWorker(workerId: string): Observable<IWorker> {
    return this._http.get<IWorker>(`${environment.API_BASE_URL}/Profile/Worker/${workerId}`);
  }

  book(booking: IBooking): Observable<Object> {
    return this._http.post(`${environment.API_BASE_URL}/Booking/book`, booking);
  }
  getAllRequest(): Observable<IBooking[]> {
    const userId = this.token.getUserId();
    return this._http.get<IBooking[]>(`${environment.API_BASE_URL}/Booking?userId=${userId}`)
      .pipe(catchError(handleError));
  }

  rescheduleBooking(booking: IBooking): Observable<Object> {
    return this._http.put(`${environment.API_BASE_URL}/Booking/${booking.id}`, booking)
    .pipe(catchError(handleError));
  }

}