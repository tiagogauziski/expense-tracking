import { Component } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { MatTableModule } from '@angular/material/table';
import { MatButton } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [MatTableModule, MatButton],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent {
  categories: Category[] = [];
  displayedColumns: string[] = ['name'];

  constructor(
    private categoryService: CategoryService,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
    })
  }

  addCategory() {
    this.router.navigate(["/categories/add"])
  }
}
