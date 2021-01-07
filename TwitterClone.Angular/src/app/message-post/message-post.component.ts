import { Component, OnInit } from '@angular/core';
import { MessageService } from '../shared/message.service';

@Component({
  selector: 'app-message-post',
  templateUrl: './message-post.component.html',
  styleUrls: ['./message-post.component.scss']
})
export class MessagePostComponent implements OnInit {

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
  }
  postComment(content:string, author:string){
    this.messageService.postMessage(content, author)
      .subscribe((response) => {
        if(response.statusText === "OK"){
          console.log(response.body);
        }
        else{
          console.log("Something went wrong")
        }
      }
      );
  }
}
