import { Component, OnInit } from '@angular/core';
import { Inventory } from '../../../model/inventory';
import { InventoryService } from '../../../service/inventory.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product } from '../../../model/product';
import { CreateUpdateTransactionDTO } from '../../../model/transaction-add-update.dto';
import { TransactionService } from '../../../service/transaction.service';
import { ProductService } from '../../../service/product.service';


@Component({
  selector: 'app-inventory',
  imports: [CommonModule, FormsModule],
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css'
})
export class InventoryComponent implements OnInit {
  inventories: Inventory[] = [];
  products: Product[] = [];

  newTransaction: CreateUpdateTransactionDTO = {
    type: true,
    quantity: 0,
    productGuid: '',
  }

  constructor(private inventoryService: InventoryService
    , private transactionService: TransactionService
    , private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    })
    this.refreshInventories();
  }

  refreshInventories() {
    this.inventoryService.getInventories().subscribe((data) => {
      this.inventories = data;
    })
  }

  addTransaction() {
    this.transactionService.addTransaction(this.newTransaction)
      .subscribe({
        next: (response) => {
          console.log('Transaction added successfully:', response);
          alert('transaction added successfully');
          this.newTransaction = { productGuid: '', quantity: 0, type: true };
          this.refreshInventories();
        },
        error: (err) => {
          console.error('Error adding transaction:', err);
          alert('Failed to add transaction. Please try again.');
        }
      })
  }
}
