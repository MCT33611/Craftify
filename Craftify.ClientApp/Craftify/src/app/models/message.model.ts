export interface Message {
    id?: string;
    conversationId: string;
    fromId: string;
    toId: string;
    content: string;
    type: MessageType;
    timestamp?: Date;
    isRead?: boolean;
    media: MessageMedia[];
  }
  
  export enum MessageType {
    Text,
    Media
  }
  
  export interface MessageMedia {
    id: string;
    messageId: string;
    fileName: string;
    contentType: string;
    fileSize: number;
    storagePath: string;
    type: MediaType;
  }
  
  export enum MediaType {
    Image,
    Video,
    Audio,
    Document
  }