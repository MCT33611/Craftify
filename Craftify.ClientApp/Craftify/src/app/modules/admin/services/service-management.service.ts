import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment.prod';
import { Observable } from 'rxjs';
import { IService } from '../../../core/models/iservice';

@Injectable({
  providedIn: 'root'
})
export class ServiceManagementService {
  private readonly apiUrl = `${environment.API_BASE_URL}/api/services`;

  constructor(private http: HttpClient) {}

  getAllServices(): Observable<IService[]> {
    return this.http.get<IService[]>(`${this.apiUrl}`);
  }

  getServiceById(id: number): Observable<IService> {
    return this.http.get<IService>(`${this.apiUrl}/${id}`);
  }

  createService(request: IService): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}`, request);
  }

  updateService(id: string, request: IService): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deleteService(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
