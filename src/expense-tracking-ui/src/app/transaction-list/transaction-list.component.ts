import { DatePipe, CurrencyPipe } from '@angular/common';
import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatOption } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { Transaction } from '../models/transaction.model';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { TransactionService } from '../services/transaction.service';

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [MatButton, MatOption, MatTableModule, DatePipe, CurrencyPipe],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.css'
})
export class TransactionListComponent {
  transactionColumns: string[] = ['type', 'category', 'details', 'amount', 'date'];
  transactionList?: Transaction[];
  categoryList?: Category[];
  
  constructor(
    private readonly transactionService: TransactionService,
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

    this.transactionService.getAll()
      .subscribe(transactionList => {
        this.transactionList = transactionList;
      });
  }

  onTransactionClick(row: Transaction) {
    
  }
}
