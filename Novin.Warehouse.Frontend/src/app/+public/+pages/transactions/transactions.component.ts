import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../../model/transaction';
import { TransactionService } from '../../../service/transaction.service';
import { CreateUpdateTransactionDTO } from '../../../model/transaction-add-update.dto';
import { Product } from '../../../model/product';
import { ProductService } from '../../../service/product.service';
import { FormsModule } from '@angular/forms';
import { ReinitializeFlowbiteDirective } from '../../../directives/reinitializeFlowbite.directive';

@Component({
  selector: 'app-transactions',
  imports: [CommonModule, FormsModule, ReinitializeFlowbiteDirective],
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

  updatedTransaction: CreateUpdateTransactionDTO = {
    type: true,
    quantity: 0,
    productGuid: '',
  }

  selectedTransactionGuid: string = '';

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
    this.transactionService.addTransaction(this.newTransaction)
      .subscribe({
        next: (response) => {
          console.log('Transaction added successfully:', response);
          alert('transaction added successfully');
          this.newTransaction = { productGuid: '', quantity: 0, type: true };
          this.refreshTransactions();
        },
        error: (err) => {
          console.error('Error adding transaction:', err);
          alert('Failed to add transaction. Please try again.');
        }
      })
  }

  updateTransaction() {
    this.transactionService.updateTransaction(this.selectedTransactionGuid, this.updatedTransaction)
      .subscribe({
        next: (response) => {
          console.log('Transaction updated successfully:', response);
          alert('Transaction updated successfully');
          this.refreshTransactions(); 
          this.updatedTransaction = { productGuid: '', quantity: 0, type: true };
          this.selectedTransactionGuid = '';
        },
        error: (err) => {
          console.error('Error updating transaction:', err);
          alert('Failed to update transaction. Please try again.');
        }
      })
  }

  removeTransaction() {
    this.transactionService.deleteTransaction(this.selectedTransactionGuid)
      .subscribe({
        next: () => {
          console.log('Transaction deleted successfully.');
          alert('Transaction deleted successfully');
          this.selectedTransactionGuid = '';
          this.refreshTransactions(); 
        },
        error: (err) => {
          console.error('Error deleting transaction:', err);
          alert('Failed to delete transaction. Please try again.');
        }
      })
  }
}
