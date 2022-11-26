import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Room } from 'src/app/models/room.model';

@Injectable({
  providedIn: 'root'
})

export class SharedMediaStreamerApiService {
  private httpClient: HttpClient;
  private url: string = "<sharedMediaStreamerApiUrl>";

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public getAllRooms(): Observable<Room> {
    return this.httpClient.get<Room>(this.url + "/api/room");
  }

  public getRoom(roomId: string): Observable<Room> {
    return this.httpClient.get<Room>(this.url + "/api/room/" + roomId).pipe();
  }

  public postRoom(): Observable<string> {
    debugger;
    return this.httpClient.post(this.url + "/api/room", null, {responseType: 'text'}).pipe(catchError(error => {
      console.log('Post request failed')
      return '';
    }))
  }

  public getSubtitles(): Observable<string> {
    return this.httpClient.post(this.url + "/api/media/subtitles", null, {responseType: 'text'}).pipe(catchError(error => {
      console.log('Post request failed')
      return '';
    }))
  }
}
