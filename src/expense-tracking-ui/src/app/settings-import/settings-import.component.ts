import { Component } from '@angular/core';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { SettingsService } from '../services/settings.service';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-settings-import',
  standalone: true,
  imports: [MatButton, MatIcon, MatButtonModule],
  templateUrl: './settings-import.component.html',
  styleUrl: './settings-import.component.css'
})
export class SettingsImportComponent {

  categoryFileName = '';
  importRuleFileName = '';

  constructor(private settingsService: SettingsService) {  }

  public deleteAllCategories() {
    this.settingsService.deleteCategories().subscribe();
  }

  public deleteAllImportRules() {
    this.settingsService.deleteImportRules().subscribe();
    
  }

  onCategoryFileSelected(event: any) {
    const file:File = event.target.files[0];
    if (file) {
        this.categoryFileName = file.name;
        const formData = new FormData();
        formData.append("thumbnail", file);
        // const upload$ = this.http.post("/api/thumbnail-upload", formData);
        // upload$.subscribe();
    }
  }

  onImportRuleFileSelected(event: any) {
    const file:File = event.target.files[0];
    if (file) {
        this.importRuleFileName = file.name;
        const formData = new FormData();
        formData.append("thumbnail", file);
        // const upload$ = this.http.post("/api/thumbnail-upload", formData);
        // upload$.subscribe();
    }
  }
}
