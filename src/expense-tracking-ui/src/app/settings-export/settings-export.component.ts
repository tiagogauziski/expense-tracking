import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { SettingsService } from '../services/settings.service';
import { saveAs } from 'file-saver';

@Component({
    selector: 'app-settings-export',
    imports: [MatButton],
    templateUrl: './settings-export.component.html',
    styleUrl: './settings-export.component.css'
})
export class SettingsExportComponent {
  constructor(private settingsService: SettingsService) {
  }
  
  public exportCategories() {
    this.settingsService.getExportCategories()
      .subscribe(data => saveAs(data, "category-export.csv"));
  }

  public exportImportRules() {
    this.settingsService.getExportImportRules()
    .subscribe(data => saveAs(data, "import-rule-export.csv"));
  }
}
