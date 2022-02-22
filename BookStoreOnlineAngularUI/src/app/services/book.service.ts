import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../Models/book-model';
import { Search } from '../Models/search-model';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http:HttpClient) { }

  readonly ApiUrl = "https://localhost:44325/api";

  getBooksByCategory(cat:string): Observable<Book[]>
  {
    var obj = {
      category: cat
    };
    return this.http.post<Book[]>(this.ApiUrl + "/transactions/categories", obj);
  }

  getBookByTitle(title: string): Observable<Book>
  {
    let book: Search = {
      bookTitle: title
    };
    return this.http.post<Book>(this.ApiUrl + "/transactions/search", book);
  }

}
