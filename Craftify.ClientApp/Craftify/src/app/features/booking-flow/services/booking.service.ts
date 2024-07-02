import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { IWorker } from '../../../models/iworker';
import { environment } from '../../../../environments/environment';
import { handleError } from '../../../shared/utils/handleError';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private _http : HttpClient) { }
  getAllWorkers(): Observable<IWorker[]> {
    return this._http.get<IWorker[]>(`${environment.API_BASE_URL}/Profile/workers`)
    .pipe(catchError(handleError));
  }
}
