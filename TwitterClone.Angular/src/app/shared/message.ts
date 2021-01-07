import { Injectable } from "@angular/core";
import { Adapter } from "./adapter";

export class Message{
    constructor(
        public author: string,
        public content: string,
        public postDate: Date){}
}

@Injectable({
    providedIn: "root",
  })
  export class MessageAdapter implements Adapter<Message> {
    adapt(item: any): Message {
      return new Message(item.author, item.content, item.postDate);
    }
  }