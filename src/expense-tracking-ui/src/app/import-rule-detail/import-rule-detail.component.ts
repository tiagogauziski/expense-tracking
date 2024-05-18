import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryDeleteDialogComponent } from '../category-delete-dialog/category-delete-dialog.component';
import { ImportRuleService } from '../services/import-rule.service';
import { ImportRule } from '../models/import-rule.model';
import { MatOption } from '@angular/material/core';
import { MatSelect } from '@angular/material/select';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { Transaction } from '../models/transaction.model';

@Component({
  selector: 'app-import-rule-detail',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButton, MatSelect, MatOption],
  templateUrl: './import-rule-detail.component.html',
  styleUrl: './import-rule-detail.component.css'
})
export class ImportRuleDetailComponent {
  id: string = "";
  NEW_IMPORT_RULE_MESSAGE_DURATION_SECONDS: number = 5;
  categoryList: Category[] = []

  importRuleForm = new FormGroup({
    id: new FormControl('0', []),
    name: new FormControl('', [Validators.required]),
    condition: new FormControl('', [Validators.required]),
    categoryId: new FormControl('', [Validators.required]),
  });

  constructor(
    private readonly importRuleService: ImportRuleService,
    private readonly categoryService: CategoryService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog) { }

  ngOnInit(): void {

    const currentNav = this.router.lastSuccessfulNavigation;
    console.log(currentNav?.extras.info);
    const importDetail: Transaction  = currentNav?.extras.info as Transaction;

    if (importDetail != undefined && importDetail.details != undefined) {
      var details = importDetail.details;

      // Only get first 12 chars of string, to be compatible with Visa Purchase types.
      if (details.length > 12) {
        details = details.slice(0, 12);
      }

      this.importRuleForm.patchValue({
        id: undefined,
        name: undefined,
        condition: `Details.StartsWith("${details}")`,
        categoryId: undefined,
      } as ImportRule)
    }

    this.categoryService
      .getCategories()
      .subscribe(categories => {
        this.categoryList = categories
      })
    this.activatedRoute.params.subscribe(params => {
      this.id = params['id'];

      if (this.id !== undefined) {
        this.importRuleService
          .getById(this.id)
          .subscribe(model => {
            this.importRuleForm.patchValue(model);
          });
      }
    });
  }

  onSubmit() {
    if (this.id !== undefined) {
      this.importRuleService
        .put(this.id, this.importRuleForm.value as ImportRule)
        .subscribe(model => {
          this.snackBar.open("Import Rule has been saved successfuly.", undefined, { duration: this.NEW_IMPORT_RULE_MESSAGE_DURATION_SECONDS * 1000 });
          this.router.navigate(["/import-rules"]);
        })
    }
    else {
      this.importRuleService
        .post(this.importRuleForm.value as ImportRule)
        .subscribe(model => {
          this.snackBar.open("Import Rule has been saved successfuly.", undefined, { duration: this.NEW_IMPORT_RULE_MESSAGE_DURATION_SECONDS * 1000 });
          this.router.navigate(["/import-rules"]);
        })
    }
  }

  onDelete() {
    const dialogRef = this.dialog.open(CategoryDeleteDialogComponent, {
      data: { category: this.importRuleForm.value },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`The dialog was closed with result: ${result}`);
      if (result == true) {
        this.importRuleService
          .delete(this.id)
          .subscribe(() => {
            this.snackBar.open("Import Rule has been deleted.", undefined, { duration: this.NEW_IMPORT_RULE_MESSAGE_DURATION_SECONDS * 1000 });
            this.router.navigate(["/import-rules"]);
          })
      }
    });
  }
}
