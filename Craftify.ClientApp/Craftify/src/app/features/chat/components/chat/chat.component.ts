import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { Conversation, User } from '../../../../models/conversation.model';
import { ChatService } from '../../services/chat.service';
import { IApiResponse } from '../../../../models/api-response.models';
import { AlertService } from '../../../../services/alert.service';
import { TokenService } from '../../../../services/token.service';
import { Message, MessageType } from '../../../../models/message.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit, OnDestroy {
  convs: Conversation[] = []
  userId!: string;
  selectedConv: Conversation | null = null;
  private subscriptions: Subscription[] = [];

  chat = inject(ChatService);
  alert = inject(AlertService);
  token = inject(TokenService);

  ngOnInit(): void {
    this.userId = this.token.getUserId()!;
    this.chat.startConnection();
    this.loadConversations();
    this.initializeChatListeners();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
    this.chat.stopConnection();
  }

  loadConversations(): void {
    const sub = this.chat.getConversations().subscribe({
      next: (response: IApiResponse<Conversation>) => {
        this.convs = response.$values;
      },
      error: error => {
        this.alert.error(error.status + ': ' + error.messages);
      }
    });
    this.subscriptions.push(sub);
  }

  initializeChatListeners(): void {
    this.chat.messageReceivedListener((message: Message) => {
      // Update conversation list or current conversation
      this.updateConversationWithNewMessage(message);
    });

    this.chat.conversationMarkedAsReadListener((conversationId: string) => {
      // Update unread count for the conversation
      this.updateConversationReadStatus(conversationId);
    });
  }

// In chat.component.ts

onSelect(conv: Conversation) {
  this.selectedConv = conv;
  this.chat.joinConversation(conv.roomId)
    .then(() => {
      console.log('Successfully joined conversation:', conv.roomId);
    })
    .catch(error => {
      this.alert.error('Failed to join conversation: ' + error);
    });
}

// Remove the startConnection call from here, as it's now handled in the service

  onSend(content: string) {
    if (this.selectedConv) {
      const msg: Message = {
        conversationId: this.selectedConv.id,
        fromId: this.userId,
        toId: this.otherUser(this.selectedConv)?.id!,
        type: MessageType.Text,
        content,
        timestamp: new Date(),
        isRead: false,
        media: []
      };
      this.chat.sendMessage(msg).catch(error => {
        this.alert.error('Failed to send message: ' + error);
      });
    }
  }

  otherUser(conv: Conversation): User | null {
    if (!this.userId) {
      console.warn('Current user ID is not set');
      return null;
    }

    return conv.peerOneId === this.userId ? conv.peerTwo : conv.peerOne;
  }

  private updateConversationWithNewMessage(message: Message): void {
    // Implement logic to update conversation list or current conversation
  }

  private updateConversationReadStatus(conversationId: string): void {
    // Implement logic to update unread count for the conversation
  }
}