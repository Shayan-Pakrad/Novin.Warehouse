import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../model/product';
import { CreateUpdateProductDTO } from '../model/product-add-update.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5166/api/ProductApi'
  
  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/list`);
  }

  addProduct(newProduct: CreateUpdateProductDTO): Observable<Product> {
    return this.http.post<Product>(`${this.apiUrl}/add`, newProduct);
  }

  updateProduct(productGuid: string, updatedProduct: CreateUpdateProductDTO): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/update/${productGuid}`, updatedProduct);
  }

  deleteProduct(productGuid: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/remove/${productGuid}`);
  }
}
