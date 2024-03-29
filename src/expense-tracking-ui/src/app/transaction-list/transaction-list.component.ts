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
import { GetAllTransactionsOptions, TransactionService } from '../services/transaction.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { provideMomentDateAdapter } from '@angular/material-moment-adapter';
import {MatChipListboxChange, MatChipsModule} from '@angular/material/chips';

import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { default as _rollupMoment } from 'moment';

const moment = _rollupMoment || _moment;

@Component({
  selector: 'app-transaction-list',
  standalone: true,
  imports: [MatButton, MatOption, MatTableModule, DatePipe, CurrencyPipe, MatDatepickerModule, MatFormFieldModule, FormsModule, ReactiveFormsModule, MatChipsModule],
  templateUrl: './transaction-list.component.html',
  styleUrl: './transaction-list.component.css',
  providers: [provideMomentDateAdapter()]
})
export class TransactionListComponent {
  transactionColumns: string[] = ['type', 'category', 'details', 'reference', 'amount', 'date'];
  transactionList?: Transaction[];
  categoryList?: Category[];

  // filter
  range = new FormGroup({
    start: new FormControl<any>(moment().add("-1", "M").startOf('month'), [Validators.required]),
    end: new FormControl<any>(moment().add("-1", "M").endOf('month'), [Validators.required]),
  });
  selectedCategories: Category[] = [];

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

    this.refreshData(this.range.get("start"), this.range.get("end"), this.selectedCategories)
  }

  onTransactionClick(row: Transaction) {

  }

  onStartChange() {

  }

  onEndChange() {
    if (this.range.valid) {
      this.refreshData(this.range.get("start"), this.range.get("end"), this.selectedCategories);
    }
  }

  refreshData(startDate: any, endDate: any, selectedCategories?: Category[]) {
    var queryOptions = new GetAllTransactionsOptions();
    queryOptions.orderBy = "Date"
    queryOptions.filter = [
      `Date ge ${this.range.get("start")?.value.format("yyyy-MM-DD")}`,
      `and Date le ${this.range.get("end")?.value.format("yyyy-MM-DD")}`
     ];

    if (selectedCategories && selectedCategories.length > 0) {
      queryOptions.filter.push(`and CategoryId in (${selectedCategories.map(c => c.id).join(", ")})`)
    }

    this.transactionService.getAll(queryOptions)
      .subscribe(transactionList => {
        this.transactionList = transactionList;
      });
  }

  categoryFilterChange(changes: MatChipListboxChange) {
    this.selectedCategories = changes.value;
    this.refreshData(this.range.get("start"), this.range.get("end"), this.selectedCategories);
  }

  previousMonthClick() {
    this.range.setValue({ 
      start: this.range.get("start")?.value.add(-1, "M").startOf('month'),
      end: this.range.get("end")?.value.add(-1, "M").endOf('month')
    });

    this.refreshData(this.range.get("start"), this.range.get("end"), this.selectedCategories);
  }

  nextMonthClick() {
    this.range.setValue({ 
      start: this.range.get("start")?.value.add(1, "M").startOf('month'),
      end: this.range.get("end")?.value.add(1, "M").endOf('month')
    });

    this.refreshData(this.range.get("start"), this.range.get("end"), this.selectedCategories);
  }
}
