import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';
import { ICategory } from '../../../../../models/icategory';
import { environment } from '../../../../../../environments/environment.prod';
import { handleError } from '../../../../../shared/utils/handleError';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {


  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(
    private http: HttpClient
  ) { }

  // GET /api/Category
  getAll(): Observable<ICategory[]> {
    return this.http.get<ICategory[]>(`${environment.API_BASE_URL}/Category`, { headers: this.headers }).pipe(catchError(handleError));
  }

  // GET /api/Category/{id}
  get(id: string): Observable<ICategory> {
    return this.http.get<ICategory>(`${environment.API_BASE_URL}/Category/${id}`, { headers: this.headers }).pipe(catchError(handleError));
  }

  // POST /api/Category
  create(category: ICategory): Observable<ICategory> {
    return this.http.post<ICategory>(`${environment.API_BASE_URL}/Category`, category, { headers: this.headers }).pipe(catchError(handleError));
  }

  // PUT /api/Category/{id}
  update(Id: string, category: ICategory): Observable<ICategory> {
    return this.http.put<ICategory>(`${environment.API_BASE_URL}/Category/${Id}`, category, { headers: this.headers }).pipe(catchError(handleError));
  }

  // DELETE /api/Category/{id}
  delete(Id: string): Observable<void> {
    return this.http.delete<void>(`${environment.API_BASE_URL}/Category/${Id}`, { headers: this.headers }).pipe(catchError(handleError));
  }
}
