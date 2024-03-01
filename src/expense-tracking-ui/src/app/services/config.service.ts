import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of, tap } from 'rxjs';
import { Configuration } from '../models/configuration.model';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private configUrl: string = "assets/config.json"
  private config: Configuration = {};

  constructor(private httpClient: HttpClient) { }

  fetchConfig(): Observable<Configuration> {
    return this.httpClient
      .get<Configuration>(this.configUrl)
      .pipe(
        tap(config => { this.config = config; })
      );
  }

  getConfig() : Configuration {
    return this.config;
  }

}
