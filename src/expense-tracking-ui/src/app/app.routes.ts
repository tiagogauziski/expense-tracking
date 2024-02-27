import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'categories', component: CategoryListComponent} ,
    { path: '',   redirectTo: '/', pathMatch: 'full' },
];
