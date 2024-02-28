import { Component } from '@angular/core';
import { CategoryService } from '../category.service';
import { Category } from '../category.model';
import { MatTableModule } from '@angular/material/table';
import { MatButton } from '@angular/material/button';

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

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
    })
  }
}
