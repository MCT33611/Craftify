import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { IUser } from '../../../models/iuser';
import { TokenService } from '../../../services/token.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment.prod';
import { handleError } from '../../../shared/utils/handleError';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(
    private tokenService : TokenService,
    private http : HttpClient
  ) { }

  get(): Observable<IUser> {
    const userId = this.tokenService.getUserId()
    return this.http.get<IUser>(`${environment.API_BASE_URL}/Profile/${userId}`, { headers: this.headers });
  }

  update(user: IUser): Observable<any> {
    const userId = this.tokenService.getUserId()
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
    
    return this.http.put(`${environment.API_BASE_URL}/Profile/${userId}`,requestBody, { headers: this.headers }).pipe(
      catchError(handleError)
    );
  }

  delete(userId: string): Observable<any> {
    return this.http.delete(`${environment.API_BASE_URL}/Profile/${userId}`, { headers: this.headers }).pipe(
      catchError(handleError)
    );
  }

  uploadProfilePicture(file: File): Observable<any> {
    const userId = this.tokenService.getUserId()
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    return this.http.put<any>(`${environment.API_BASE_URL}/Profile/picture/${userId}`, formData).pipe(
      catchError(handleError)
    );
  }
}
