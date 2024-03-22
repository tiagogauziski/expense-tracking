import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';
import { ImportRule } from '../models/import-rule.model';

@Injectable({
  providedIn: 'root'
})
export class ImportRuleService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getAll(): Observable<ImportRule[]> {
    return this.httpClient.get<ImportRule[]>(`${this.configService.getConfig().baseUrl}/api/importrule`);
  }

  getById(id: string): Observable<ImportRule> {
    return this.httpClient.get<ImportRule>(`${this.configService.getConfig().baseUrl}/api/importrule/${id}`);
  }

  post(model: ImportRule): Observable<ImportRule> {
    return this.httpClient.post<ImportRule>(`${this.configService.getConfig().baseUrl}/api/importrule`, model);
  }

  put(id: string, model: ImportRule): Observable<ImportRule> {
    return this.httpClient.put<ImportRule>(`${this.configService.getConfig().baseUrl}/api/importrule/${id}`, model);
  }

  delete(id: string): Observable<any> {
    return this.httpClient.delete<any>(`${this.configService.getConfig().baseUrl}/api/importrule/${id}`);
  }
}
