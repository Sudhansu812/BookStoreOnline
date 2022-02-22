import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor {    
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {    
    const sessionData = JSON.parse(sessionStorage.getItem('jwt') || '{}');
    const token: string = sessionData.token;    
    if (token) {    
      request = request.clone({    
        setHeaders: {    
          Authorization: `Bearer ${token}`,    
        }    
      });    
    }    
    return next.handle(request)    
  }    
}     
