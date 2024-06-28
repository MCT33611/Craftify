import { Injectable } from '@angular/core';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { IUser } from '../../../models/iuser';
import { TokenService } from '../../../services/token.service';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment.prod';
import { handleError } from '../../../shared/utils/handleError';
import { IPlan } from '../../../models/iplan';
import { ISubscription } from '../../../models/isubscription';
import { uploadWorkerDocResponse } from '../../../models/workerDocUploadResponse';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  

  constructor(
    private _tokenService: TokenService,
    private _http: HttpClient
  ) { }

  get(): Observable<IUser> {
    const userId = this._tokenService.getUserId()
    return this._http.get<IUser>(`${environment.API_BASE_URL}/Profile/${userId}`);
  }

  update(user: IUser): Observable<Object> {
    const userId = this._tokenService.getUserId()
    const requestBody = {
      "firstName": user.firstName,
      "lastName": user.lastName,
      "streetAddress": user.streetAddress,
      "city": user.city,
      "state": user.state,
      "postalCode": user.postalCode,
      "profilePicture": user.profilePicture
    }
    console.log(requestBody);

    return this._http.put(`${environment.API_BASE_URL}/Profile/${userId}`, requestBody).pipe(
      catchError(handleError)
    );
  }

  delete(userId: string): Observable<Object> {
    return this._http.delete(`${environment.API_BASE_URL}/Profile/${userId}`).pipe(
      catchError(handleError)
    );
  }

  uploadProfilePicture(file: File): Observable<string> {
    const userId = this._tokenService.getUserId()
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    return this._http.put<string>(`${environment.API_BASE_URL}/Profile/picture/${userId}`, formData).pipe(
      catchError(handleError)
    );
  }

  getAllPlans(): Observable<IPlan[]> {
    return this._http.get<IPlan[]>(`${environment.API_BASE_URL}/Plan`).pipe(
      catchError(handleError)
    );
  }

  Subscribe(subscription: ISubscription) {
    return this._http.post(environment.API_BASE_URL + `/Profile/Subscribe`, subscription).pipe(
      catchError(handleError)
    );
  }

  uploadWorkerDoc(file: File): Observable<uploadWorkerDocResponse> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    const headers = new HttpHeaders({
      'accept': '*/*'
    });
    return this._http.put<uploadWorkerDocResponse>(
      `${environment.API_BASE_URL}/Profile/Worker/Upload/Doc`,
      formData,
      { headers: headers }
    ).pipe(
      catchError(handleError)
    );
  }
}
