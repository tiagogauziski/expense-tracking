import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormField, MatLabel } from '@angular/material/input';
import { MatSelect, MatOption } from '@angular/material/select';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DashboardService } from '../services/dashboard.service';

@Component({
  selector: 'app-dashboard-year-summary',
  imports: [MatCardModule, MatButtonModule, NgxChartsModule, MatSelect, MatFormField, MatLabel, MatOption],
  templateUrl: './dashboard-year-summary.component.html',
  styleUrl: './dashboard-year-summary.component.css',
})
export class DashboardYearSummaryComponent {
  summaryChatView: any = [500, 500];
  summaryDataSource: any = [];

  yearList: any[] = [
    {value: '2023'},
    {value: '2024'},
    {value: '2025'},
    {value: '2026'},
  ];
  selectedYear = this.yearList[2].value;
  selectedCategories: string[] = [];

  constructor(
    private readonly dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.refreshData(this.selectedYear);
  }

  refreshData(year: string): void {
    this.dashboardService
      .getSummaryPerYear(year)
      .subscribe(summaryData => {
        this.summaryDataSource = summaryData;
      });
  }

  selectYear() {
    this.refreshData(this.selectedYear);
  }
}
