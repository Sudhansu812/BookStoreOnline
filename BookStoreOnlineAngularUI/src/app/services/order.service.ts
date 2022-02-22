import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Order } from '../Models/order-model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  formData: Order;
  readonly ApiUrl = "https://localhost:44325/api";

  constructor(
    private http: HttpClient
  ) { }

  order(order: Order)
  {
    console.log(order.orderQuantity);
    return this.http.post<boolean>(this.ApiUrl+"/transactions/order", order);
  }

  getRemailningStock(id: number)
  {
    return this.http.get<number>(this.ApiUrl+"/transactions/orders/" + id);
  }

  // The following methods are for refreshing the grid even after closing the
  // dialog box.
  private _listeners = new Subject<any>();
  listen(): Observable<any> {
    return this._listeners.asObservable();
  }
  filter(filterBy: string) {
    this._listeners.next(filterBy);
  }
}
