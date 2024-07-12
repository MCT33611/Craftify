// chat.component.ts

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChatService } from '../../../services/chat.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-chat',
  standalone:true,
  imports:[CommonModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  senderId!: string;
  receiverId!: string;
  messages: any[] = [];
  newMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private chatService: ChatService
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.senderId = params['senderId'];
      this.receiverId = params['receiverId'];
      this.initializeChat();
    });
  }

  initializeChat() {
    this.chatService.joinRoom(this.senderId, this.receiverId)
      .then(() => {
        this.chatService.getMessages().subscribe(messages => {
          this.messages = messages;
        });
      });
  }

  sendMessage() {
    if (this.newMessage.trim()) {
      this.chatService.sendMessage(this.receiverId, this.newMessage);
      this.newMessage = '';
    }
  }
}