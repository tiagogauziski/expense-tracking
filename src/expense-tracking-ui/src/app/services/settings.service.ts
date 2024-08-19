import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Category } from '../models/category.model';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';


@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }
  
  getExportCategories(): Observable<any> {
    return this.httpClient.get(`${this.configService.getConfig().baseUrl}/api/settings/export/categories`, { responseType: 'blob' });
  }

  getExportImportRules(): Observable<any> {
    return this.httpClient.get(`${this.configService.getConfig().baseUrl}/api/settings/export/import-rules`, { responseType: 'blob' });
  }
}
  