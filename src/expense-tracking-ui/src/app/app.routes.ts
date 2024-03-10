import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { ImportListComponent } from './import-list/import-list.component';
import { ImportFileComponent } from './import-file/import-file.component';
import { ImportDetailComponent } from './import-detail/import-detail.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'categories', component: CategoryListComponent },
    { path: 'categories/add', component: CategoryDetailComponent },
    { path: 'categories/edit/:id', component: CategoryDetailComponent },
    { path: 'imports', component: ImportListComponent },
    { path: 'imports/file', component: ImportFileComponent },
    { path: 'imports/detail/:id', component: ImportDetailComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
];
