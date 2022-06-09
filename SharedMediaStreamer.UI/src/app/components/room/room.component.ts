import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { NamePromptComponent } from './name-prompt/name-prompt.component';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { ChatMessageEvent } from 'src/app/models/chat-message-event';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class RoomComponent implements OnInit {
  roomId!: string;
  username!: string;
  videoSource!: string;
  messages: string = "";

  constructor(private currentRoute: ActivatedRoute, private dialog: MatDialog) {
    currentRoute.params.subscribe(urlParamters => {
      this.roomId = urlParamters['roomId']
    });
  }

  ngOnInit(): void {
    this.dialog.open(NamePromptComponent, {disableClose: true}).afterClosed().subscribe(usernameInput => {
      if (usernameInput != null || usernameInput != "")
      {
        this.username = usernameInput;
        this.setVideoSource();
        this.websocketInit();
      }      
    });
  }

  private setVideoSource() {
    this.videoSource = "http://192.168.0.82:5000/api/media";
  }

  private websocketInit() {
    const websocketUrl = "ws://192.168.0.82:5000/?username=" + this.username + "&room=" + this.roomId;
    console.log("Joining websocket server");
    let websocket = webSocket<ChatMessageEvent>(websocketUrl);
    console.log(websocket);
    
    websocket.asObservable().subscribe({
      next: (value: ChatMessageEvent) => {
        this.messages += `<p>${value.message}</p>`;
      }
    })
  }
}