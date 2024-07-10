// chat.component.ts
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ChatService } from '../../../services/chat.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IBooking } from '../../../models/ibooking';

@Component({
  selector: 'app-chat',
  standalone:true,
  imports:[
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {
  serviceRequestId!: string;
  serviceRequest!:IBooking;
  messages: any[] = [];
  newMessage = '';
  private messagesSubscription!: Subscription;

  constructor(private chatService: ChatService, private route: ActivatedRoute) {}

  async ngOnInit() {
    this.serviceRequestId = this.route.snapshot.paramMap.get('serviceRequestId')!;
    if(this.serviceRequestId){
      this.chatService.getRequest(this.serviceRequestId).subscribe(res => this.serviceRequest = res)
    }
    try {
      await this.chatService.ensureConnection();
      await this.chatService.joinServiceRequestChat(this.serviceRequestId);
      this.loadMessages();

      this.messagesSubscription = this.chatService.messages$.subscribe(messages => {
        this.messages = messages;
      });
    } catch (error) {
      console.error('Failed to initialize chat:', error);
      // Handle the error (e.g., show an error message to the user)
    }
  }

  async ngOnDestroy() {
    try {
      await this.chatService.leaveServiceRequestChat(this.serviceRequestId);
    } catch (error) {
      console.error('Error leaving chat:', error);
    }
    if (this.messagesSubscription) {
      this.messagesSubscription.unsubscribe();
    }
  }

  loadMessages() {
    this.chatService.getMessages(this.serviceRequestId).subscribe(
      messages => {
        this.messages = messages;
      },
      error => console.error('Error loading messages:', error)
    );
  }

  async sendMessage() {
    if (this.newMessage.trim()) {
      const message = {
        senderId: this.serviceRequest.providerId, // Replace with actual user ID
        receiverId: this.serviceRequest.customerId, // Replace with the ID of the other user in the chat
        content: this.newMessage,
        serviceRequestId: this.serviceRequestId
      };
      try {
        await this.chatService.sendMessage(message);
        this.newMessage = '';
      } catch (error) {
        console.error('Error sending message:', error);
        // Handle the error (e.g., show an error message to the user)
      }
    }
  }
}