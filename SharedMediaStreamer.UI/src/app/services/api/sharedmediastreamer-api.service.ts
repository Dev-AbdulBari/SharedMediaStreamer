import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Room } from 'src/app/models/room.model';

@Injectable({
  providedIn: 'root'
})

export class SharedMediaStreamerApiService {
  private httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public getAllRooms(): Observable<Room> {
    return this.httpClient.get<Room>("http://192.168.0.82:5000/api/room");
  }

  public getRoom(roomId: string) {
    return this.httpClient.get<Room>("http://192.168.0.82:5000/api/room/" + roomId);
  }

  public postRoom(): Observable<string> {
    return this.httpClient.post("http://192.168.0.82:5000/api/room", null, {responseType: 'text'}).pipe(catchError(error => {
      console.log('Post request failed')
      return '';
    }))
  }
}
