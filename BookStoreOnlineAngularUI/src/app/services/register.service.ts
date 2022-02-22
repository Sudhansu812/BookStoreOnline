import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserLogin } from '../Models/user-login';
import { User } from '../Models/user-model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) { }

  formData: User;

  readonly ApiUrl = "https://localhost:44325/api";

  addUser(user: User) {
    return this.http.post(this.ApiUrl + '/accounts', user);
  }

  logUser(user: UserLogin)
  {
    return this.http.post(this.ApiUrl + "/accounts/login", user).pipe(map(
      res => {
        const user = res;
        if(user)
        {
          console.log(res);
          sessionStorage.setItem("jwt",JSON.stringify(user));
        }
      }
    ));
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
