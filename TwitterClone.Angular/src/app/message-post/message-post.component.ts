import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from '../shared/message';
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

  loading: boolean;
  
  ngOnInit(): void {
    this.errors = [];
  }
  onSubmit(f: NgForm) {
    this.postComment(f.value.contentArea, f.value.authorField);
    f.resetForm();
  }
  onPostSuccess(){
    this.successfulPost.emit()
    this.loading = false;
  }
 
  postComment(content:string, author:string){
    this.loading = true;
    this.errors = [];
    this.messageService.postMessage(content, author)
      .subscribe(
        (response) => {
        if(response.statusText === "OK"){
          this.onPostSuccess();
        }}, 
        (httpError) => {
            this.loading = false;
            if (httpError.status === 400) {
              this.transformErrors(httpError.error.errors);
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

