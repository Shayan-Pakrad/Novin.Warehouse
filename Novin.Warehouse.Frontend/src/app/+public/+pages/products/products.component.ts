import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../../model/product';
import { ProductService } from '../../../service/product.service';

@Component({
  selector: 'app-products',
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) { }
  
  ngOnInit(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    })
  }
  
}
