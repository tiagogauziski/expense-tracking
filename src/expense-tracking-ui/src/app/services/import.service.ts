import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Import } from '../models/import.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ConfigService } from './config.service';
import { ImportFile } from '../models/import-file.model';
import { Transaction } from '../models/transaction.model';

export class GetAllImportOptions {
  expand?: string;
  filter?: string[];
  orderBy?: string;
}

export class GetImportByIdOptions {
  expand?: string = "transactions($expand=category)";
  filter?: string[];
  orderBy?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ImportService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getAll(queryOptions: GetAllImportOptions = new GetAllImportOptions()): Observable<Import[]> {
    const options = { params: new HttpParams() };
    if (queryOptions.expand) {
      options.params = options.params.append("$expand", queryOptions.expand!)
    }
    if (queryOptions.filter) {
      options.params = options.params.append("$filter", queryOptions.filter!.join(" "))
    }
    if (queryOptions.orderBy) {
      options.params = options.params.append("$orderby", queryOptions.orderBy!)
    }

    return this.httpClient.get<Import[]>(`${this.configService.getConfig().baseUrl}/api/import`, options);
  }

  getById(id: string, queryOptions: GetAllImportOptions = new GetAllImportOptions()): Observable<Import> {
    const options = { params: new HttpParams() };
    if (queryOptions.expand) {
      options.params = options.params.append("$expand", queryOptions.expand!)
    }
    if (queryOptions.filter) {
      options.params = options.params.append("$filter", queryOptions.filter!.join(" "))
    }
    if (queryOptions.orderBy) {
      options.params = options.params.append("$orderby", queryOptions.orderBy!)
    }

    return this.httpClient.get<Import>(`${this.configService.getConfig().baseUrl}/api/import/${id}`, options);
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

  editImportTransaction(importId: string, id: string, model: Transaction): Observable<any> {
    return this.httpClient.put<Import>(`${this.configService.getConfig().baseUrl}/api/import/${importId}/transaction/${id}`, model);
  }

  executeImport(id: string, operation: string): Observable<any> {
    return this.httpClient.patch<any>(`${this.configService.getConfig().baseUrl}/api/import/${id}/execute/${operation}`, undefined);
  }
}
