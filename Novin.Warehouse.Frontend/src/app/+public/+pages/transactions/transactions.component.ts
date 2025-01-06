import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../../model/transaction';
import { TransactionService } from '../../../service/transaction.service';
import { CreateUpdateTransactionDTO } from '../../../model/transaction-add-update.dto';
import { Product } from '../../../model/product';
import { ProductService } from '../../../service/product.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-transactions',
  imports: [CommonModule, FormsModule],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent implements OnInit{
  transactions: Transaction[] = [];
  products: Product[] = [];

  newTransaction: CreateUpdateTransactionDTO = {
    type: true,
    quantity: 0,
    productGuid: '',
  }

  constructor(private transactionService: TransactionService
    , private productService: ProductService) { }
  
  ngOnInit(): void {
    this.refreshTransactions();
    
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    })
  }

  refreshTransactions() {
    this.transactionService.getTransactions().subscribe((data) => {
      this.transactions = data;
    })
  }

  addTransaction() {
    console.log(this.newTransaction);
    this.transactionService.addTransaction(this.newTransaction)
      .subscribe({
        next: (response) => {
          console.log('Transaction added successfully:', response);
          alert('transaction added successfully');
          this.newTransaction = { productGuid: '', quantity: 0, type: true };
          this.refreshTransactions();
        }
      })
  }
}
