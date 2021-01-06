import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Message } from '../shared/message';
import { MessageService } from '../shared/message.service';

@Component({
  selector: 'app-message-view',
  templateUrl: './message-view.component.html',
  styleUrls: ['./message-view.component.scss']
})
export class MessageViewComponent implements OnInit {

  constructor(private messageService: MessageService) { }

  messages$: Observable<Message[]>;

  ngOnInit(): void {
    this.messages$ = this.messageService.getMessages();
  }

}
