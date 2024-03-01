import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Category } from '../models/category.model';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getCategories(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(`${this.configService.getConfig().baseUrl}/api/category`);
  }
}
