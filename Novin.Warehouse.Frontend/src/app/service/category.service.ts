import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../model/category';
import { CreateUpdateCategoryDTO } from '../model/category-add-update.dto';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'http://localhost:5166/api/CategoryApi'

  constructor(private http: HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl + '/list');
  }

  addCategory(newCategory: CreateUpdateCategoryDTO): Observable<Category> {
    return this.http.post<Category>(this.apiUrl + '/add', newCategory);
  }
}
