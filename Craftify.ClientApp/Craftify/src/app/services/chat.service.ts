// chat.service.ts
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, catchError, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IBooking } from '../models/ibooking';
import { environment } from '../../environments/environment';
import { handleError } from '../shared/utils/handleError';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection: signalR.HubConnection;
  private messagesSubject = new BehaviorSubject<any[]>([]);
  public messages$ = this.messagesSubject.asObservable();
  private connectionPromise: Promise<void>;

  constructor(private http: HttpClient) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.API_CHAT_URL,{
        withCredentials:true
      })
      .withAutomaticReconnect()
      .build();

    this.connectionPromise = this.start();

    this.hubConnection.on('ReceiveMessage', (message: any) => {
      const currentMessages = this.messagesSubject.value;
      this.messagesSubject.next([...currentMessages, message]);
    });
  }

  private async start() {
    try {
      await this.hubConnection.start();
      console.log('SignalR connection started');
    } catch (err) {
      console.error('Error while starting SignalR connection:', err);
      // Retry connection
      setTimeout(() => this.start(), 5000);
    }
  }

  async ensureConnection() {
    if (this.hubConnection.state === signalR.HubConnectionState.Connected) {
      return Promise.resolve();
    } else {
      return this.connectionPromise;
    }
  }

  async joinServiceRequestChat(serviceRequestId: string) {
    await this.ensureConnection();
    return this.hubConnection.invoke('JoinServiceRequestChat', serviceRequestId);
  }

  async leaveServiceRequestChat(serviceRequestId: string) {
    await this.ensureConnection();
    return this.hubConnection.invoke('LeaveServiceRequestChat', serviceRequestId);
  }

  async sendMessage(message: any) {
    await this.ensureConnection();
    return this.hubConnection.invoke('SendMessage', message);
  }

  getMessages(serviceRequestId: string): Observable<any[]> {
    return this.http.get<any[]>(`${environment.API_BASE_URL}/chat/${serviceRequestId}`, { withCredentials: true });
  }
  
  getRequest(id:string): Observable<IBooking> {
    return this.http.get<IBooking>(`${environment.API_BASE_URL}/Booking/${id}`, { withCredentials: true })
      .pipe(catchError(handleError));
  }
}