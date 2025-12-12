import { Component } from '@angular/core';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { SettingsService } from '../services/settings.service';
import { MatIcon } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-settings-import',
    imports: [MatButton, MatIcon, MatButtonModule],
    templateUrl: './settings-import.component.html',
    styleUrl: './settings-import.component.css'
})
export class SettingsImportComponent {

  categoryFile?: File;
  importRuleFile?: File;

  constructor(
    private settingsService: SettingsService,
    private snackBar: MatSnackBar
  ) {  }

  public deleteAllCategories() {
    this.settingsService.deleteCategories().subscribe();
  }

  public deleteAllImportRules() {
    this.settingsService.deleteImportRules().subscribe();
    
  }

  onCategoryFileSelected(event: any) {
    const file:File = event.target.files[0];
    if (file) {
        this.categoryFile = file;
    }
  }

  onImportRuleFileSelected(event: any) {
    const file:File = event.target.files[0];
    if (file) {
        this.importRuleFile = file;
    }
  }

  uploadFiles() {
    if (this.categoryFile && this.importRuleFile) {
      this.settingsService.import(this.categoryFile, this.importRuleFile).subscribe(() => {
        this.snackBar.open("Category and import rules imported successfully.", undefined, { duration: 5000 });
      })
    }
    else {
      this.snackBar.open("Select category and import rule files before importing.", undefined, { duration: 5000 });
    }
  }
}
