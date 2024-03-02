import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'categories', component: CategoryListComponent },
    { path: 'categories/add', component: CategoryDetailComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
];
