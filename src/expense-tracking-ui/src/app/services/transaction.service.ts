import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ConfigService } from './config.service';
import { Transaction } from '../models/transaction.model';

export class GetAllTransactionsOptions {
  expand: string = "category";
  filter?: string;
  orderBy?: string;
}

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getAll(queryOptions: GetAllTransactionsOptions = new GetAllTransactionsOptions()): Observable<Transaction[]> {
    var uri: string = `${this.configService.getConfig().baseUrl}/api/transaction`
    const options = { params: new HttpParams() };
    if (queryOptions.expand) {
      options.params = options.params.append("$expand", queryOptions.expand!)
    }
    if (queryOptions.filter) {
      options.params = options.params.append("$filter", queryOptions.filter!)
    }
    if (queryOptions.orderBy) {
      options.params = options.params.append("$orderby", queryOptions.orderBy!)
    }

    return this.httpClient.get<Transaction[]>(`${this.configService.getConfig().baseUrl}/api/transaction`, options);
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
