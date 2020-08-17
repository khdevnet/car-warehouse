import { Injectable, EventEmitter, Inject } from '@angular/core';
import  * as signalR from '@microsoft/signalr';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class CarsWarehouseService {
  constructor(
    private readonly http: HttpClient,
    @Inject("BASE_URL") private readonly baseUrl: string
  ) {}

  getCars(): Observable<string[]> {

  return this.http.get<string[]>(this.baseUrl + "api/warehouse/cars");
  }
}
