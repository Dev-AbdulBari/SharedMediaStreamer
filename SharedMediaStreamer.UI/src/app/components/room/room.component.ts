import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { NamePromptComponent } from './name-prompt/name-prompt.component';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { ChatMessageEvent } from 'src/app/models/chat-message-event';
import { MessageEventType } from 'src/app/enums/message-event-type';
import { SharedMediaStreamerApiService } from 'src/app/services/api/sharedmediastreamer-api.service';
import { first } from 'rxjs';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class RoomComponent implements OnInit {
  @ViewChild('videoPlayer') videoPlayer!: ElementRef;

  roomId!: string;
  username!: string;
  websocket!: WebSocketSubject<ChatMessageEvent>;
  videoSource!: string;
  messages: string = "";
  chatMessageInput: string = "";
  users: string[] = [];
  isVideoEvent: boolean = false;
  url: string = "<SharedMediaStreamerApiUrl>";
  subtitleUrl: string = this.url + "/api/media/subtitles";
  websocketUrl: string = "<SharedMediaStreamerApiWebsocketUrl>";

  constructor(private currentRoute: ActivatedRoute, private dialog: MatDialog, private apiService: SharedMediaStreamerApiService) {
    this.currentRoute.params.subscribe(urlParamters => {
      this.roomId = urlParamters['roomId']
    });
  }

  ngOnInit(): void {
    this.dialog.open(NamePromptComponent, { disableClose: true }).afterClosed().subscribe(usernameInput => {
      this.username = usernameInput;
      this.setVideoSource();
      this.websocketInit();      
    });
  }

  sendMessage(matInput: HTMLInputElement): void {
    console.log(matInput.value);
    this.websocket.next({ messageEventType: MessageEventType.Message, message: matInput.value, user: this.username })
    matInput.value = "";
  }

  seekedEventHandler() {
    if (!this.isVideoEvent) {
      console.log("Seeked at: " + this.videoPlayer.nativeElement.currentTime)
      this.websocket.next({ messageEventType: MessageEventType.VideoTimeSeek, message: this.videoPlayer.nativeElement.currentTime, user: this.username });
    }

    this.isVideoEvent = false;
  }

  playEventHandler(): void {
    if (!this.isVideoEvent) {
      console.log("played at : " + this.videoPlayer.nativeElement.currentTime);
      this.websocket.next({ messageEventType: MessageEventType.VideoTimePlay, message: this.videoPlayer.nativeElement.currentTime, user: this.username });
    }

    this.isVideoEvent = false;
  }

  pauseEventHandler(): void {
    if (!this.isVideoEvent) {
      console.log("paused at : " + this.videoPlayer.nativeElement.currentTime);
      this.websocket.next({ messageEventType: MessageEventType.VideoTimePause, message: this.videoPlayer.nativeElement.currentTime, user: this.username });
    }

    this.isVideoEvent = false;
  }

  private setVideoSource() {
    this.videoSource = this.url + "/api/media";
  }

  private websocketInit() {
    const websocketUrl = this.websocketUrl + "/?username=" + this.username + "&room=" + this.roomId;
    this.websocket = webSocket<ChatMessageEvent>(websocketUrl);
    console.log(this.websocket);

    this.websocket.subscribe(
      value => { this.chatEventHandler(value) }
    )
  }

  private chatEventHandler(chatEvent: ChatMessageEvent): void {
    if (chatEvent.messageEventType == MessageEventType.Message) {
      this.messages += `<p class="message">${chatEvent.user}: ${chatEvent.message}</p>`;
    } else if (chatEvent.messageEventType == MessageEventType.Broadcast) {
      this.messages += `<p class="broadcast">${chatEvent.message}</p>`;
      this.getRoomInformation();
    } else if (chatEvent.messageEventType == MessageEventType.VideoTimePlay) {
      this.messages += `<p class="broadcast">${chatEvent.user} has played the video</p>`;
      this.isVideoEvent = true;
      this.videoPlayer.nativeElement.currentTime = chatEvent.message;
      this.videoPlayer.nativeElement.play();
    } else if (chatEvent.messageEventType == MessageEventType.VideoTimePause) {
      this.messages += `<p class="broadcast">${chatEvent.user} has paused the video</p>`;
      this.isVideoEvent = true;
      this.videoPlayer.nativeElement.currentTime = chatEvent.message;
      this.videoPlayer.nativeElement.pause();
    } else if (chatEvent.messageEventType == MessageEventType.VideoTimeSeek) {
      this.messages += `<p class="broadcast">${chatEvent.user} has skipped the video</p>`;
      this.isVideoEvent = true;
      this.videoPlayer.nativeElement.currentTime = chatEvent.message;
    }

    setTimeout(this.handleScroll, 1);
  }

  private getRoomInformation(): void {
    this.apiService.getRoom(this.roomId).pipe(first()).subscribe(currentRoom => {
      this.users = [];
      currentRoom.users.forEach(x => this.users.push(x.userName));
    });
  }

  handleScroll(): void {
    var messageChat = document.getElementById("messages");

    messageChat!.scrollTop = messageChat!.scrollHeight;
  }

  getUsers(): void {
    this.messages += `<p class="broadcast"> Active users: <br/> ${this.users.join("<br/>")}</p>`;
    setTimeout(this.handleScroll, 1);
  }
}