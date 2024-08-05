import { Component, inject, Input, OnChanges, OnInit, OnDestroy, ElementRef, AfterViewInit, AfterViewChecked, ViewChild, NgZone } from '@angular/core';
import { Conversation } from '../../../../models/conversation.model';
import { Message, MessageType } from '../../../../models/message.model';
import { ChatService } from '../../services/chat.service';
import { IApiResponse } from '../../../../models/api-response.models';

interface List {
  id?: string,
  content: string,
  time?: Date,
  type: MessageType,
  isMy: boolean,
  isDeleted: boolean
}

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent implements OnChanges, AfterViewChecked {
  @ViewChild('scrollContainer') private scrollContainer!: HTMLDivElement;
  @Input({ required: true }) conversation!: Conversation;
  @Input({ required: true }) currentUserId!: string;
  messages: List[] = [];
  chat = inject(ChatService);

  constructor(private ngZone: NgZone) { }
  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }
  ngOnChanges(): void {
    this.loadMessages();
    this.setupListeners();
  }

  scrollToBottom(): void {
    this.scrollContainer.scrollTop= this.scrollContainer.scrollHeight;
  }


  loadMessages(): void {
    this.chat.getMessagesByConversationId(this.conversation.id).subscribe({
      next: (response: { $id: string, messages: IApiResponse<Message> }) => {
        this.messages = response.messages.$values
          .map((msg: Message): List => ({
            id: msg.id,
            content: msg.content,
            isMy: msg.fromId === this.currentUserId,
            time: msg.timestamp ? new Date(msg.timestamp) : undefined,
            type: msg.type,
            isDeleted: msg.content === ''
          }))
          .sort((a, b) => {
            if (!a.time) return 1;
            if (!b.time) return -1;
            return a.time.getTime() - b.time.getTime();
          });
      },
      error: (err: any) => console.error('Error loading messages:', err)
    });
  }

  setupListeners(): void {
    this.chat.messageReceivedListener(newMsg => {
      const msg: List = {
        content: newMsg.content,
        isMy: newMsg.fromId === this.currentUserId,
        time: newMsg.timestamp,
        type: newMsg.type,
        isDeleted: false
      };
      this.messages.push(msg);
    });

    this.chat.messageDeletedListener(deletedMsgId => {
      const index = this.messages.findIndex(m => m.id === deletedMsgId);
      if (index !== -1) {
        this.messages[index].isDeleted = true;
        this.messages[index].content = '';
      }
    });

    this.chat.messageUpdatedListener(updatedMsg => {
      const index = this.messages.findIndex(m => m.id === updatedMsg.id);
      if (index !== -1) {
        this.messages[index] = {
          ...this.messages[index],
          content: updatedMsg.content,
          time: updatedMsg.timestamp
        };
      }
    });
  }
}


