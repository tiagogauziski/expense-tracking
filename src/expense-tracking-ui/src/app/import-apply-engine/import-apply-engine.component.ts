import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ProgressBarMode, MatProgressBarModule } from '@angular/material/progress-bar';
import { ImportService } from '../services/import.service';
import { firstValueFrom, from, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-import-apply-engine',
  standalone: true,
  imports: [MatButton, MatProgressBarModule, FormsModule, MatFormFieldModule, MatInputModule],
  templateUrl: './import-apply-engine.component.html',
  styleUrl: './import-apply-engine.component.css'
})
export class ImportApplyEngineComponent {
  progress: number = 0;
  executionProgressText = "";

  constructor(
    private readonly importService: ImportService) { }


  async applyEngine() {
    // Initialise variables
    this.progress = 0;
    this.executionProgressText = "";
    this.updateExecutionProgress("Initialising...");

    // Load all imports
    this.updateExecutionProgress("Loading all imports...");

    var imports = await firstValueFrom(this.importService.getAll());

    this.updateExecutionProgress(`Loaded ${imports.length} imports...`);

    var totalImports = imports.length;
    var index: number = 1;

    for (let index = 0; index < imports.length; index++) {
      const element = imports[index];

      this.progress = totalImports / index;
      this.updateExecutionProgress(`Executing engine for import ${element.name}`);

      var execution = await firstValueFrom(this.importService.executeImport(element.id!, "reimport"));

      this.updateExecutionProgress(`Executing engine for import ${element.name}...done`);
    }

    this.progress = 100;
    this.updateExecutionProgress(`Finished.`);
  }

  updateExecutionProgress(status: string) {
    this.executionProgressText += status;
    this.executionProgressText += "\n";
  }
}
