import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { ImportListComponent } from './import-list/import-list.component';
import { ImportFileComponent } from './import-file/import-file.component';
import { ImportDetailComponent } from './import-detail/import-detail.component';
import { ImportRuleListComponent } from './import-rule-list/import-rule-list.component';
import { ImportRuleDetailComponent } from './import-rule-detail/import-rule-detail.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { ImportApplyEngineComponent } from './import-apply-engine/import-apply-engine.component';
import { SettingsHomeComponent } from './settings-home/settings-home.component';
import { SettingsExportComponent } from './settings-export/settings-export.component';
import { SettingsImportComponent } from './settings-import/settings-import.component';
import { DashboardHomeComponent } from './dashboard-home/dashboard-home.component';

export const routes: Routes = [
    { path: 'home', component: DashboardHomeComponent },
    { path: 'categories', component: CategoryListComponent },
    { path: 'categories/add', component: CategoryDetailComponent },
    { path: 'categories/edit/:id', component: CategoryDetailComponent },
    { path: 'import-rules', component: ImportRuleListComponent },
    { path: 'import-rules/add', component: ImportRuleDetailComponent },
    { path: 'import-rules/edit/:id', component: ImportRuleDetailComponent },
    { path: 'imports', component: ImportListComponent },
    { path: 'imports/file', component: ImportFileComponent },
    { path: 'imports/apply-engine', component: ImportApplyEngineComponent },
    { path: 'imports/detail/:id', component: ImportDetailComponent },
    { path: 'transactions', component: TransactionListComponent },
    { 
        path: 'settings', 
        component: SettingsHomeComponent, 
        children: [
            { path: 'export', component: SettingsExportComponent },
            { path: 'import', component: SettingsImportComponent },
        ] 
    },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
];
