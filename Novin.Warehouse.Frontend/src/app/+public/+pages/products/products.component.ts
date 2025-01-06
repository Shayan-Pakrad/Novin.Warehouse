import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../../model/product';
import { ProductService } from '../../../service/product.service';
import { CategoryService } from '../../../service/category.service';
import { Category } from '../../../model/category';
import { CreateUpdateProductDTO } from '../../../model/product-add-update.dto';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-products',
  imports: [CommonModule, FormsModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];

  newProduct: CreateUpdateProductDTO = {
    name: '',
    price: 0,
    categoryGuid: '',
    description: '',
    minQuantity: 0,
  }

  updatedProduct: CreateUpdateProductDTO = {
    name: '',
    price: 0,
    categoryGuid: '',
    description: '',
    minQuantity: 0,
  }

  selectedProductGuid: string = '';

  constructor(private productService: ProductService
    , private categoryService: CategoryService) { }
  
  ngOnInit(): void {
    this.refreshProducts();
    this.categoryService.getCategories().subscribe((data) => {
      this.categories = data;
    })
  }

  refreshProducts() {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    })
  }

  addProduct() {
    this.productService.addProduct(this.newProduct).subscribe({
      next: (response) => {
        console.log('Product added successfully:', response);
        alert('product added successfully');
        this.newProduct = { name: '', description: '', categoryGuid: '', minQuantity: 0, price: 0 };
        this.refreshProducts();
      }
    })
  }

  updateProduct() {
    console.log(this.updatedProduct)
    this.productService.updateProduct(this.selectedProductGuid, this.updatedProduct)
      .subscribe({
        next: (response) => {
          console.log('Product updated successfully:', response);
          alert('Product updated successfully');
          this.refreshProducts(); 
          this.updatedProduct = { name: '', description: '', categoryGuid: '', minQuantity: 0, price: 0 };
          this.selectedProductGuid = '';
        },
        error: (err) => {
          console.error('Error updating product:', err);
          alert('Failed to update product. Please try again.');
        }
      })
  }
  
}
