import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Inventory } from '../model/inventory';


@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  private apiUrl = 'localhost:5166/api/InventoryApi/list';

  constructor(private http: HttpClient) { }

  getInventories(): Observable<Inventory[]> {
    return this.http.get<Inventory[]>(this.apiUrl);
  }
}
