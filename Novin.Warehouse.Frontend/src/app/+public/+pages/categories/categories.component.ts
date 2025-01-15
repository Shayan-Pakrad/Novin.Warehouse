import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Category } from '../../../model/category';
import { CategoryService } from '../../../service/category.service';
import { FormsModule } from '@angular/forms';
import { CreateUpdateCategoryDTO } from '../../../model/category-add-update.dto';
import { ReinitializeFlowbiteDirective } from '../../../directives/reinitializeFlowbite.directive';

@Component({
  selector: 'app-categories',
  imports: [CommonModule, FormsModule, ReinitializeFlowbiteDirective],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit {
  // forms properties
  newCategory: CreateUpdateCategoryDTO = {
    name: '',
    description: '',
  };

  updatedCategory: CreateUpdateCategoryDTO = {
    name: '',
    description: '',
  }

  selectedCategoryGuid: string = '';


  categories: Category[] = [];

  constructor(private categoryService: CategoryService) { }
  
  ngOnInit(): void {
    this.refreshCategories();
  }

  refreshCategories() {
    this.categoryService.getCategories().subscribe((data) => {
      this.categories = data;
    })
  }

  addCategory() {
    this.categoryService.addCategory(this.newCategory).subscribe({
      next: (response) => {
        console.log('Category added successfully:', response);
        alert('category added successfully');
        this.newCategory = { name: '', description: '' };
        this.refreshCategories();
      },
      error: (err) => {
        console.error('Error adding category:', err);
        alert('Failed to add category. Please try again.');
      },
    })
  }

  updateCategory() {
    this.categoryService.updateCategory(this.selectedCategoryGuid, this.updatedCategory)
      .subscribe({
        next: (response) => {
          console.log('Category updated successfully:', response);
          alert('Category updated successfully');
          this.refreshCategories(); 
          this.updatedCategory = { name: '', description: '' };
          this.selectedCategoryGuid = '';
        },
        error: (err) => {
          console.error('Error updating category:', err);
          alert('Failed to update category. Please try again.');
        }
      })
  }

  removeCategory() {
    this.categoryService.deleteCategory(this.selectedCategoryGuid)
    .subscribe({
      next: () => {
        console.log('Category deleted successfully.');
        alert('Category deleted successfully');
        this.selectedCategoryGuid = '';
        this.refreshCategories(); 
      },
      error: (err) => {
        console.error('Error deleting category:', err);
        alert('Failed to delete category. Please try again.');
      }
    });
  }
}
