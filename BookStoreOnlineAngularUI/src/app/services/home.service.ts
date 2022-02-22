import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Book } from '../Models/book-model';
import { BookRating } from '../Models/rating-model';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) { }

  formData: Book;

  readonly ApiUrl = "https://localhost:44325/api";

  mostSoldBooks$ = this.getBookList(1).pipe(shareReplay(1));
  trendingBooks$ = this.getBookList(2).pipe(shareReplay(1));
  recentBooks$ = this.getBookList(3).pipe(shareReplay(1));

  getBookList(id:number)
  {
    console.log(id);
    return this.http.get<Book[]>(this.ApiUrl + '/transactions/categories/' + id);
  }

  rateBook(r: number, bId: number, uId: number)
  {
    let rate: BookRating = {
      userId: uId,
      bookId: bId,
      rating: r
    }
    return this.http.put(this.ApiUrl + '/transactions/ratebook', rate);
  }

  private _listeners = new Subject<any>();
  listen():Observable<any>{
    return this._listeners.asObservable();
  }
  filter(filterBy:string)
  {
    this._listeners.next(filterBy);
  }
  
}
