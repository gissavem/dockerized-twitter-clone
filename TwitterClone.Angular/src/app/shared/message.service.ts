import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
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
  getMessages(): Observable<any>{
    return this.http.get(this.apiUrl + "/messages",).pipe(
      map ((data: any[]) => data.map((item: any) => this.adapter.adapt(item)) ));
  }

  postMessage(value: string): Observable<any>{
    let body = 
    {
      "content": value
    }
    return this.http.post(this.apiUrl+"/",body, { observe: 'response' })
  }
}
