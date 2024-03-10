import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatOption, MatSelect } from '@angular/material/select';
import { ImportService } from '../services/import.service';
import { ImportFile } from '../models/import-file.model';
import { Import } from '../models/import.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-import-file',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButton, MatSelect, MatOption],
  templateUrl: './import-file.component.html',
  styleUrl: './import-file.component.css'
})
export class ImportFileComponent {
  importFileForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    layout: new FormControl('', [Validators.required]),
  });
  importFileSelection: File | undefined = undefined;

  constructor(private importService: ImportService, private router: Router) { }

  onSubmit(): void {
    this.importService
      .postImport(this.importFileForm.value as Import, this.importFileSelection as File)
      .subscribe(model => {
        this.router.navigate([`imports/detail/${model.id}`])
      })
  }

  onFileSelected() {
    const inputNode: any = document.querySelector('#file');

    this.importFileSelection = inputNode.files[0];
  }
}
