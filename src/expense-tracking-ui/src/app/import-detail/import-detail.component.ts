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
import {MatCheckboxModule} from '@angular/material/checkbox';

@Component({
  selector: 'app-import-detail',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButton, MatSelect, MatOption, MatTableModule, DatePipe, CurrencyPipe, MatCheckboxModule],
  templateUrl: './import-detail.component.html',
  styleUrl: './import-detail.component.css'
})
export class ImportDetailComponent {
  MESSAGE_DURATION_SECONDS: number = 5;

  importId: string = "";
  importModel?: Import = undefined;
  importForm = new FormGroup({
    name: new FormControl('', []),
    layout: new FormControl('', []),
    createdAt: new FormControl('', []),
    isExecuted: new FormControl({value: '', disabled: true}, []),
    executedAt: new FormControl('', []),
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

    dialogRef.afterClosed().subscribe(selectedCategory => {
      console.log(`The dialog was closed with result: ${selectedCategory}`);
      if (selectedCategory == undefined) {
        return;
      }
      else if (selectedCategory.id == 0){
        row.categoryId = undefined;
      }
      else {
        row.categoryId = selectedCategory.id;
      }
      this.importService.editImportTransaction(this.importId, row.id, row)
        .subscribe(result => {
          row.category = selectedCategory;
          this.snackBar.open("Transaction has been saved successfuly.", undefined, { duration: this.MESSAGE_DURATION_SECONDS * 1000 });
      })
    });
  }

  onImport() {
    this.importService.executeImport(this.importId).subscribe(result => {
      this.snackBar.open("Import has been executed successfully.", undefined, { duration: this.MESSAGE_DURATION_SECONDS * 1000 });
      this.router.navigate(['transactions']);
    });
  }
}
