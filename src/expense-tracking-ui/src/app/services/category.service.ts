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

  getCategoryById(categoryId: string): Observable<Category> {
    return this.httpClient.get<Category>(`${this.configService.getConfig().baseUrl}/api/category/${categoryId}`);
  }

  postCategory(model: Category): Observable<Category> {
    return this.httpClient.post<Category>(`${this.configService.getConfig().baseUrl}/api/category`, model);
  }

  putCategory(categoryId: string, model: Category): Observable<Category> {
    return this.httpClient.put<Category>(`${this.configService.getConfig().baseUrl}/api/category/${categoryId}`, model);
  }

  deleteCategory(categoryId: string): Observable<any> {
    return this.httpClient.delete<any>(`${this.configService.getConfig().baseUrl}/api/category/${categoryId}`);
  }
}
