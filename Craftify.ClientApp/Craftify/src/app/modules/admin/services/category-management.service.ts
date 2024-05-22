import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment.prod';
import { Observable } from 'rxjs';
import { ICategory } from '../../../core/models/icategory';

@Injectable({
  providedIn: 'root'
})
export class CategoryManagementService {
  private readonly apiUrl = `${environment.API_BASE_URL}/category`;

  constructor(private http: HttpClient) {}

  getAllCategories(): Observable<ICategory[]> {
    return this.http.get<ICategory[]>(`${this.apiUrl}`);
  }

  getCategoryById(id: number): Observable<ICategory> {
    return this.http.get<ICategory>(`${this.apiUrl}/${id}`);
  }

  createCategory(request: ICategory): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}`, request);
  }

  updateCategory(id: string, request: ICategory): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deleteCategory(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
