import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Upload } from '../Models/upload-model';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  constructor(private http: HttpClient) { }

  formData: Upload;

  readonly ApiUrl = "https://localhost:44325/api";

  uploadBook(book: Upload)
  {
    return this.http.post(this.ApiUrl + '/transactions/addbook', book);
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
