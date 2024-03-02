import { Component } from '@angular/core';
import { FormControl, Validators, FormsModule, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButton } from '@angular/material/button';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-category-detail',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButton],
  templateUrl: './category-detail.component.html',
  styleUrl: './category-detail.component.css'
})
export class CategoryDetailComponent {
  constructor(private readonly categoryService: CategoryService, private snackBar: MatSnackBar) { }

  categoryForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
  });

  onSubmit() {
    this.categoryService
      .postCategory(this.categoryForm.value as Category)
      .subscribe(category => {
        this.snackBar.open("Category has been saved successfuly.");
      })
  }
}
