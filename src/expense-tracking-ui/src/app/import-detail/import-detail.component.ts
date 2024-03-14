import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatOption } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelect } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ImportService } from '../services/import.service';
import { Import } from '../models/import.model';
import { MatTableModule } from '@angular/material/table';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { ImportCategorySelectionDialogComponent, ImportCategorySelectionDialogData } from '../import-category-selection-dialog/import-category-selection-dialog.component';
import { Transaction } from '../models/transaction.model';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-import-detail',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButton, MatSelect, MatOption, MatTableModule, DatePipe, CurrencyPipe],
  templateUrl: './import-detail.component.html',
  styleUrl: './import-detail.component.css'
})
export class ImportDetailComponent {
  importId: string = "";
  importModel?: Import = undefined;
  importForm = new FormGroup({
    name: new FormControl('', []),
    layout: new FormControl('', []),
    createdAt: new FormControl('', []),
  });
  transactionColumns: string[] = ['type', 'category', 'details', 'amount', 'date'];
  categoryList?: Category[];

  constructor(
    private readonly importService: ImportService,
    private readonly categoryService: CategoryService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.categoryService.getCategories()
      .subscribe(categoryList => {
        this.categoryList = categoryList;
      });

    this.activatedRoute.params.subscribe(params => {
      this.importId = params['id'];

      if (this.importId !== undefined) {
        this.importService
          .getImportById(this.importId)
          .subscribe(model => {
            this.importModel = model;
            this.importForm.patchValue(this.importModel);
          });
      }
    });
  }

  onTransactionClick(row: Transaction): void {
    const dialogRef = this.dialog.open(ImportCategorySelectionDialogComponent, {
      data: {
        categoryList: this.categoryList,
        selectedTransaction: row
      } as ImportCategorySelectionDialogData,
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`The dialog was closed with result: ${result}`);
      row.category = result;
    });
  }
}