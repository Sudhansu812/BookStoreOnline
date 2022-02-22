import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Cart } from '../Models/cart-model';
import { CartList } from '../Models/cart-read-model';
import { CartTable } from '../Models/cart-table-model';
import { OrderCart } from '../Models/order-cart-model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  
  formData: Cart;
  readonly ApiUrl = "https://localhost:44325/api";

  constructor(
    private http: HttpClient
  ) { }

  addToCart(cart: Cart)
  {
    return this.http.post<boolean>(this.ApiUrl+"/transactions/addtocart", cart);
  }

  orderCartItems(id: number)
  {
    let cart: OrderCart = {
      userId: id
    }
    return this.http.post<boolean>(this.ApiUrl+"/transactions/ordercartitems", cart)
  }

  getCartList(id: number): Observable<CartTable[]> {
    let cartTable: CartList = {
      userId: id
    }
    return this.http.post<CartTable[]>(this.ApiUrl + "/transactions/getcarttable/", cartTable);
  }

  deleteFromCart(id: number) {
    return this.http.delete(this.ApiUrl + '/transactions/deletefromcart/' + id);
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
