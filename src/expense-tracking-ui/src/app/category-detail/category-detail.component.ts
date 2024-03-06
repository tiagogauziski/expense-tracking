import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormsModule, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButton } from '@angular/material/button';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CategoryDeleteDialogComponent } from '../category-delete-dialog/category-delete-dialog.component';

@Component({
  selector: 'app-category-detail',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButton],
  templateUrl: './category-detail.component.html',
  styleUrl: './category-detail.component.css'
})
export class CategoryDetailComponent implements OnInit {
  categoryId: string = "";
  NEW_CATEGORY_MESSAGE_DURATION_SECONDS: number = 5;

  categoryForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
  });

  constructor(
    private readonly categoryService: CategoryService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.categoryId = params['id'];

      if (this.categoryId !== undefined) {
        this.categoryService
          .getCategoryById(this.categoryId)
          .subscribe(category => {
            this.categoryForm.patchValue(category);
          });
      }
    });
  }

  onSubmit() {
    if (this.categoryId !== undefined) {
      this.categoryService
        .putCategory(this.categoryId, this.categoryForm.value as Category)
        .subscribe(category => {
          this.snackBar.open("Category has been saved successfuly.", undefined, { duration: this.NEW_CATEGORY_MESSAGE_DURATION_SECONDS * 1000 });
          this.router.navigate(["/categories"]);
        })
    }
    else {
      this.categoryService
        .postCategory(this.categoryForm.value as Category)
        .subscribe(category => {
          this.snackBar.open("Category has been saved successfuly.", undefined, { duration: this.NEW_CATEGORY_MESSAGE_DURATION_SECONDS * 1000 });
          this.router.navigate(["/categories"]);
        })
    }
  }

  onDelete() {
    const dialogRef = this.dialog.open(CategoryDeleteDialogComponent, {
      data: { category: this.categoryForm.value },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`The dialog was closed with result: ${result}`);
      if (result == true) {
        this.categoryService
          .deleteCategory(this.categoryId)
          .subscribe(() => {
            this.snackBar.open("Category has been deleted.", undefined, { duration: this.NEW_CATEGORY_MESSAGE_DURATION_SECONDS * 1000 });
            this.router.navigate(["/categories"]);
          })
      }
    });
  }
}
