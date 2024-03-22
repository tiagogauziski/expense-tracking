import { Component } from '@angular/core';
import { ImportRuleService } from '../services/import-rule.service';
import { ImportRule } from '../models/import-rule.model';
import { Router } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-import-rule-list',
  standalone: true,
  imports: [MatTableModule, MatButton],
  templateUrl: './import-rule-list.component.html',
  styleUrl: './import-rule-list.component.css'
})
export class ImportRuleListComponent {
  importRules: ImportRule[] = [];
  displayedColumns: string[] = ['name', 'category'];

  constructor(
    private importRuleService: ImportRuleService,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.importRuleService
      .getAll()
      .subscribe(importRules => {
        this.importRules = importRules;
      })
  }

  addImportRule() {
    this.router.navigate(["/import-rules/add"]);
  }

  editImportRule(importRule: ImportRule) {
    this.router.navigate(["/import-rules/edit", importRule.id]);
  }
}
