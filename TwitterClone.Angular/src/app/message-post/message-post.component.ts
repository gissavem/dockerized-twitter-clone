import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MessageService } from '../shared/message.service';

@Component({
  selector: 'app-message-post',
  templateUrl: './message-post.component.html',
  styleUrls: ['./message-post.component.scss']
})
export class MessagePostComponent implements OnInit {

  errors: string[];
  constructor(private messageService: MessageService) {

   }
  @Output() successfulPost = new EventEmitter();
  
  ngOnInit(): void {
    this.errors = [];
  }

  onPostSuccess(){
    this.successfulPost.emit()
  }

  onPostFailure(){
    console.log(this.errors);
  }

 
  postComment(content:string, author:string){
    this.errors = [];
    this.messageService.postMessage(content, author)
      .subscribe(
        (response) => {
        if(response.statusText === "OK"){
          this.onPostSuccess();
        }}, 
        (httpError) => {
            if (httpError.status === 400) {
              this.transformErrors(httpError.error.errors);
              this.onPostFailure();
              }
        });
  }
  transformErrors(errorObject): void{
    for(var fieldName in errorObject){
      if (errorObject.hasOwnProperty(fieldName)) {
        this.errors.push(errorObject[fieldName][0]);
      }
    }
  }
}

