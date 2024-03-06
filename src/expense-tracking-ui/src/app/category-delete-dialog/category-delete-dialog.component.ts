import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialog,
  MatDialogRef,
  MatDialogActions,
  MatDialogClose,
  MatDialogTitle,
  MatDialogContent,
} from '@angular/material/dialog';

@Component({
  selector: 'app-category-delete-dialog',
  standalone: true,
  imports: [MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent],
  templateUrl: './category-delete-dialog.component.html',
  styleUrl: './category-delete-dialog.component.css'
})
export class CategoryDeleteDialogComponent {
  constructor(public dialogRef: MatDialogRef<CategoryDeleteDialogComponent>) {}
}
