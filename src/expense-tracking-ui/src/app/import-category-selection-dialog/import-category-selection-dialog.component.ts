import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatOption, MatSelect } from '@angular/material/select';
import { Category } from '../models/category.model';
import { Transaction } from '../models/transaction.model';

export interface ImportCategorySelectionDialogData {
  selectedTransaction: Transaction;
  categoryList: Category[];
}
@Component({
    selector: 'app-import-category-selection-dialog',
    imports: [MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MatFormFieldModule, FormsModule, MatSelect, MatOption],
    templateUrl: './import-category-selection-dialog.component.html',
    styleUrl: './import-category-selection-dialog.component.css'
})
export class ImportCategorySelectionDialogComponent {
  selectedCategory?: Category;
  unassignedCategory:Category = { id: '0', name: "(Unassigned)"}
  constructor(
    public dialogRef: MatDialogRef<ImportCategorySelectionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ImportCategorySelectionDialogData,
  ) {
    this.selectedCategory = data.selectedTransaction.category;
  }

  onCancel() {
    this.dialogRef.close();
  }

  compareFn(c1: Category, c2: Category): boolean {
    return c1 && c2 ? c1.id === c2.id : c1 === c2;
}
}
