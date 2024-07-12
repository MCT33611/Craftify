
import { inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { TokenService } from './token.service';
import * as signalR from '@microsoft/signalr';
import { combineGuids } from '../shared/utils/combineGuids';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  tokenService = inject(TokenService);
  private messageSubject = new BehaviorSubject<any[]>([]);
  private hubConnection: HubConnection = new HubConnectionBuilder()
  .withUrl('https://localhost:7283/chat', {
    accessTokenFactory: () => this.tokenService.getToken() || ''
  })
  .configureLogging(signalR.LogLevel.Information)
  .build();


  constructor() {
    this.start();

    this.hubConnection.on('ReceiveMessage', (senderId: string, message: string) => {
      console.log("new msg:" + message);

      const currentMessages = this.messageSubject.value;
      this.messageSubject.next([...currentMessages, { senderId, message }]);
    });

    this.hubConnection.on('UserOffline', (e) => {
      console.log("new msg:" + e);
    });

    this.hubConnection.on('UserOnline', (e) => {
      console.log("user online");
    });

  }

  public async start(){
    try{
      await this.hubConnection.start();
    }
    catch(error){
      console.error("error:",error);
    }
  }

  joinRoom(senderId: string, receiverId: string): Promise<void> {
    const roomId = combineGuids(senderId,receiverId);
    return this.hubConnection.invoke('JoinRoom', roomId)
      .catch(err => console.error(err));
  }

  public sendMessage(receiverId: string, message: string): void {
    this.hubConnection.invoke('SendMessage', receiverId, message)
      .catch(err => console.error(err));
  }




  public getMessages(): Observable<any[]> {
    return this.messageSubject.asObservable();
  }
}