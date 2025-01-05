import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../../model/transaction';
import { TransactionService } from '../../../service/transaction.service';

@Component({
  selector: 'app-transactions',
  imports: [CommonModule],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit{
  transactions: Transaction[] = [];

  constructor(private transactionService: TransactionService) { }
  
  ngOnInit(): void {
    this.transactionService.getTransactions().subscribe((data) => {
      this.transactions = data;
    })
  }
}
