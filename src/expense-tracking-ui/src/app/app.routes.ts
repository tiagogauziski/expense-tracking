import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { ImportListComponent } from './import-list/import-list.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'categories', component: CategoryListComponent },
    { path: 'categories/add', component: CategoryDetailComponent },
    { path: 'categories/edit/:id', component: CategoryDetailComponent },
    { path: 'imports', component: ImportListComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
];
