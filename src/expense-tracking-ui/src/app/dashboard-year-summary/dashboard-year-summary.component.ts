import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DashboardService } from '../services/dashboard.service';
import { MatFormField, MatLabel, MatOption, MatSelect } from '@angular/material/select';
import { MatChipListboxChange, MatChipsModule } from '@angular/material/chips';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';

@Component({
    selector: 'app-dashboard-year-summary',
    imports: [MatCardModule, MatButtonModule, NgxChartsModule, MatSelect, MatFormField, MatLabel, MatOption, MatChipsModule],
    templateUrl: './dashboard-year-summary.component.html',
    styleUrl: './dashboard-year-summary.component.css'
})
export class DashboardYearSummaryComponent {
  summaryChatView: any = [500, 500];
  summaryDataSource: any = [];
  categoryList?: Category[];

  yearList: any[] = [
    {value: '2023'},
    {value: '2024'},
    {value: '2025'},
  ];
  selectedYear = this.yearList[2].value;
  selectedCategories: string[] = [];

  constructor(
    private readonly dashboardService: DashboardService,
    private readonly categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategories()
      .subscribe(categoryList => {
        this.categoryList = categoryList;
      });

    this.refreshData(this.selectedYear);
  }

  refreshData(year: string, categories?: string[]): void {
    this.dashboardService
      .getSummaryPerYear(year, categories)
      .subscribe(summaryData => {
        this.summaryDataSource = summaryData;
      });
  }

  selectYear() {
    this.refreshData(this.selectedYear, this.selectedCategories);
  }

  categoryFilterChange(changes: MatChipListboxChange) {
    this.selectedCategories = changes.value;
    this.refreshData(this.selectedYear, this.selectedCategories);
  }
}
