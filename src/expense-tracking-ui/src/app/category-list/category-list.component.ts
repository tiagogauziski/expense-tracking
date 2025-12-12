import { Component, ViewChild } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButton } from '@angular/material/button';
import { Router } from '@angular/router';
import { MatSort, MatSortable, MatSortModule } from '@angular/material/sort';

@Component({
    selector: 'app-category-list',
    imports: [MatTableModule, MatButton, MatSortModule],
    templateUrl: './category-list.component.html',
    styleUrl: './category-list.component.css'
})
export class CategoryListComponent {
  displayedColumns: string[] = ['name'];
  dataSource: MatTableDataSource<Category> = new MatTableDataSource();

  @ViewChild(MatSort) sort?: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort!;
  }

  constructor(
    private categoryService: CategoryService,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.categoryService
      .getCategories()
      .subscribe(categories => {
        this.dataSource.data = categories;
      })
  }

  addCategory() {
    this.router.navigate(["/categories/add"]);
  }

  editCategory(category: Category) {
    this.router.navigate(["/categories/edit", category.id]);
  }
}
