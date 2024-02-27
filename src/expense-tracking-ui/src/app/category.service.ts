import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Category } from './category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  categories: Category[] = [
    {id: '1', name: 'Supermarket'},
    {id: '2', name: 'Rent'}
  ];

  constructor() { }

  getCategories(): Observable<Category[]> {
    const categories = of(this.categories);
    return categories;
  }
}
