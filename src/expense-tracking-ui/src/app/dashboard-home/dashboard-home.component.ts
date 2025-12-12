import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DashboardService } from '../services/dashboard.service';
import { MatFormField, MatLabel, MatOption, MatSelect } from '@angular/material/select';
import { DashboardYearInteractiveComponent } from '../dashboard-year-interactive/dashboard-year-interactive.component';
import { DashboardYearSummaryComponent } from '../dashboard-year-summary/dashboard-year-summary.component';

@Component({
    selector: 'app-dashboard-home',
    imports: [DashboardYearInteractiveComponent, DashboardYearSummaryComponent],
    templateUrl: './dashboard-home.component.html',
    styleUrl: './dashboard-home.component.css'
})
export class DashboardHomeComponent {

}
