import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DashboardService } from '../services/dashboard.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, NgxChartsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  summaryChatView: any = [500, 500];
  summaryDataSource: any = [];

  constructor(
    private readonly dashboardService: DashboardService) { }

  ngOnInit(): void {
      this.dashboardService
        .getSummaryPerYear("2024")
        .subscribe(summaryData => {
          this.summaryDataSource = summaryData;
        });
  }
}
