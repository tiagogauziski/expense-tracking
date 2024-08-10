import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-settings-home',
  standalone: true,
  imports: [MatSidenavModule, MatListModule, MatIconModule, RouterOutlet],
  templateUrl: './settings-home.component.html',
  styleUrl: './settings-home.component.css'
})
export class SettingsHomeComponent {

}
