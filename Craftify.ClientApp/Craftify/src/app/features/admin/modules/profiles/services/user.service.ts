import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from '../../../../../services/token.service';
import { Observable } from 'rxjs';
import { IUser } from '../../../../../models/iuser';
import { environment } from '../../../../../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(
    private tokenService: TokenService,
    private http: HttpClient
  ) { }

  get(userId:string): Observable<IUser> {
    return this.http.get<IUser>(`${environment.API_BASE_URL}/Profile/${userId}`, { headers: this.headers });
  }

  getAll(): Observable<IUser[]> {
    return this.http.get<IUser[]>(`${environment.API_BASE_URL}/Profile`, { headers: this.headers });
  }
}
