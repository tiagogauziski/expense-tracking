import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Import } from '../models/import.model';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';
import { ImportFile } from '../models/import-file.model';

@Injectable({
  providedIn: 'root'
})
export class ImportService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getImports(): Observable<Import[]> {
    return this.httpClient.get<Import[]>(`${this.configService.getConfig().baseUrl}/api/import`);
  }

  getImportById(ImportId: string): Observable<Import> {
    return this.httpClient.get<Import>(`${this.configService.getConfig().baseUrl}/api/import/${ImportId}`);
  }

  postImport(model: Import, file: File): Observable<Import> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    formData.append('name', model.name);
    formData.append('layout', model.layout);

    return this.httpClient.post<Import>(`${this.configService.getConfig().baseUrl}/api/import`, formData);
  }

  putImport(ImportId: string, model: Import): Observable<Import> {
    return this.httpClient.put<Import>(`${this.configService.getConfig().baseUrl}/api/import/${ImportId}`, model);
  }

  deleteImport(ImportId: string): Observable<any> {
    return this.httpClient.delete<any>(`${this.configService.getConfig().baseUrl}/api/import/${ImportId}`);
  }
}
