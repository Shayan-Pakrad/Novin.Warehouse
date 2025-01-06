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
    price: -1,
    categoryGuid: '',
    description: '',
    minQuantity: 0,
  }

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
    console.log(this.newProduct);
    this.productService.addProduct(this.newProduct).subscribe({
      next: (response) => {
        console.log('Product added successfully:', response);
        alert('product added successfully');
        this.newProduct = { name: '', description: '', categoryGuid: '', minQuantity: 0, price: -1 };
        this.refreshProducts();
      }
    })
  }
  
}
