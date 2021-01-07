import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MessageService } from '../shared/message.service';

@Component({
  selector: 'app-message-post',
  templateUrl: './message-post.component.html',
  styleUrls: ['./message-post.component.scss']
})
export class MessagePostComponent implements OnInit {

  constructor(private messageService: MessageService) { }
  @Output() successfulPost = new EventEmitter();

  onPostSuccess(){
    this.successfulPost.emit()
  }

  onPostFailure(response){
    console.log("something went wrong...");
    console.log(response);
  }

  ngOnInit(): void {
  }
  postComment(content:string, author:string){
    this.messageService.postMessage(content, author)
      .subscribe((response) => {
        if(response.statusText === "OK"){
          this.onPostSuccess();
        }
        else{
          this.onPostFailure(response);
        }
      }
      );
  }
}
