import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { MessageAdapter } from './message';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient, private adapter: MessageAdapter) {
    this.apiUrl = environment.apiUrl;
   }
  
  apiUrl: string;
  getMessages(){
    return this.http.get(this.apiUrl + "/message")
    .pipe( 
      map (
        (data: any[]) => data.map(
          (item: any) => this.adapter.adapt(item)
          )
        )
      );
    // .subscribe(
    //   (response) =>{
    //     map (
    //       (data: any[]) => data.map(
    //         (item: any) => this.adapter.adapt(item)
    //         )
    //       )
    //   },
    //   (httpError)=>{
    //     throwError(httpError);
    //   });

  }

  postMessage(content: string, author: string): Observable<any>{
    let body = 
    {
      "content": content,
      "author" : author
    }
    return this.http
      .post(this.apiUrl+"/message",body, { observe: 'response' });
    }
}

