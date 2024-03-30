import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { ImportService } from '../services/import.service';
import { Router } from '@angular/router';
import { Import } from '../models/import.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-import-list',
  standalone: true,
  imports: [MatTableModule, MatButton, DatePipe],
  templateUrl: './import-list.component.html',
  styleUrl: './import-list.component.css'
})
export class ImportListComponent {
  displayedColumns: string[] = ['name', 'layout', 'createdAt'];
  imports: Import[] = []

  constructor(
    private importService: ImportService,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.importService
      .getAll()
      .subscribe(imports => {
        this.imports = imports;
      })
  }

  import(): void {
    this.router.navigate(["imports/file"])
  }

  editImport(model: Import) {
    this.router.navigate(["/imports/detail", model.id]);
  }
}
