import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Category } from '../models/category.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getSummaryPerYearMonth(year: string, categories?: string[]): Observable<any[]> {
    var uri: string = `${this.configService.getConfig().baseUrl}/api/dashboard/summary-year-month/${year}`
    const options = { params: new HttpParams() };
    if (categories) {
      categories.forEach(category => {
        options.params = options.params.append("categories", category);
      });
    }
    return this.httpClient.get<any[]>(uri, options);
  }

  getSummaryPerYear(year: string): Observable<any[]> {
    var uri: string = `${this.configService.getConfig().baseUrl}/api/dashboard/summary-year/${year}`
    const options = { params: new HttpParams() };
    return this.httpClient.get<any[]>(uri, options);
  }

}
