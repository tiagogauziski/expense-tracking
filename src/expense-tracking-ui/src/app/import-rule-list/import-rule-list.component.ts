import { Component, ViewChild } from '@angular/core';
import { ImportRuleService } from '../services/import-rule.service';
import { ImportRule } from '../models/import-rule.model';
import { Router } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';

@Component({
  selector: 'app-import-rule-list',
  standalone: true,
  imports: [MatTableModule, MatButton, MatSortModule],
  templateUrl: './import-rule-list.component.html',
  styleUrl: './import-rule-list.component.css'
})
export class ImportRuleListComponent {
  displayedColumns: string[] = ['name', 'category.name'];
  dataSource: MatTableDataSource<ImportRule> = new MatTableDataSource();

  @ViewChild(MatSort) sort?: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort!;

    this.dataSource.sortingDataAccessor = (item: any, property) => {
      switch (property) {
        case 'category.name': return item.category?.name;
        default: return item[property as keyof ImportRule]; 
      }
    };
  }
  constructor(
    private importRuleService: ImportRuleService,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.importRuleService
      .getAll()
      .subscribe(importRules => {
        this.dataSource.data = importRules;
      })
  }

  addImportRule() {
    this.router.navigate(["/import-rules/add"]);
  }

  editImportRule(importRule: ImportRule) {
    this.router.navigate(["/import-rules/edit", importRule.id]);
  }
}
