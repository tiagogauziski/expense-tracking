import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Category } from '../models/category.model';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getSummaryPerYear(year: string): Observable<any[]> {
    return this.httpClient.get<any[]>(`${this.configService.getConfig().baseUrl}/api/dashboard/summary-year/${year}`);
  }

}
