import { Injectable, EventEmitter } from '@angular/core';
import  * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class NotificationHubService {
  private connection: signalR.HubConnection;

  private readonly notifications: BehaviorSubject<string> = new BehaviorSubject<string>("");
  readonly notifications$ = this.notifications.asObservable()

  constructor()
  {
    console.log("constructor");
  }

  start(): Promise<void> {
    console.log("start");
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("/notification-hub")
      .build();
    const _notifications =  this.notifications;
    this.connection.on("ReceiveMessage", function (user, message) {
      console.log("ReceiveMessage");
      console.log(message);
      _notifications.next(message);
    });

    return this.connection.start().catch(function (err) {
      return console.error(err.toString());
    });
  }
}
