import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../../../core/models/iuser';
import { TokenService } from '../../../core/services/token.service';
import { environment } from '@env/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  constructor(
    private http: HttpClient,
    private tokenService : TokenService
  ) { }

  logout() {
    this.tokenService.removeToken();
  }

  getUser(): Observable<IUser> {
    const userId = this.tokenService.getUserId();
    return this.http.get<IUser>(`${environment.API_BASE_URL}/Profile/${userId}`, { headers: this.headers });
  }

  update(user: IUser): Observable<any> {
    const userId = this.tokenService.getUserId;
    return this.http.put(`${environment.API_BASE_URL}/Profile/${userId}`, user, { headers: this.headers });
  }

  delete(): Observable<any> {
    const userId = this.tokenService.getUserId;
    return this.http.delete(`${environment.API_BASE_URL}/Profile/${userId}`, { headers: this.headers });
  }
  uploadProfilePicture(file: File): Observable<any> {
    const userId = this.tokenService.getUserId;
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    return this.http.put<any>(`${environment.API_BASE_URL}/Profile/picture/${userId}`, formData);
  }
}
