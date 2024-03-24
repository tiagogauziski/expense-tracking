import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';
import { Transaction } from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getAll(): Observable<Transaction[]> {
    return this.httpClient.get<Transaction[]>(`${this.configService.getConfig().baseUrl}/api/transaction`);
  }

  getById(id: string): Observable<Transaction> {
    return this.httpClient.get<Transaction>(`${this.configService.getConfig().baseUrl}/api/transaction/${id}`);
  }

  post(model: Transaction): Observable<Transaction> {
    return this.httpClient.post<Transaction>(`${this.configService.getConfig().baseUrl}/api/transaction`, model);
  }

  put(id: string, model: Transaction): Observable<Transaction> {
    return this.httpClient.put<Transaction>(`${this.configService.getConfig().baseUrl}/api/transaction/${id}`, model);
  }

  delete(id: string): Observable<any> {
    return this.httpClient.delete<any>(`${this.configService.getConfig().baseUrl}/api/transaction/${id}`);
  }
}
