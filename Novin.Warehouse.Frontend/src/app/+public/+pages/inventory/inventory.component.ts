import { Component, OnInit } from '@angular/core';
import { Inventory } from '../../../model/inventory';
import { InventoryService } from '../../../service/inventory.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-inventory',
  imports: [CommonModule],
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css'
})
export class InventoryComponent implements OnInit {
  inventories: Inventory[] = [];

  constructor(private inventoryService: InventoryService) { }

  ngOnInit(): void {
    this.inventoryService.getInventories().subscribe((data) => {
      this.inventories = data;
    })
  }
}
