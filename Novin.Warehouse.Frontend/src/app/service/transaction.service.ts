import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../model/transaction';
import { CreateUpdateTransactionDTO } from '../model/transaction-add-update.dto';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = 'http://localhost:5166/api/TransactionApi'

  constructor(private http: HttpClient) { }

  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.apiUrl}/list`);
  }

  addTransaction(newTransaction: CreateUpdateTransactionDTO): Observable<Transaction> {
    return this.http.post<Transaction>(`${this.apiUrl}/add`, newTransaction);
  }

  updateTransaction(transactionGuid: string, updatedTransaction: CreateUpdateTransactionDTO): Observable<Transaction> {
    return this.http.put<Transaction>(`${this.apiUrl}/update/${transactionGuid}`, updatedTransaction);
  }
}
