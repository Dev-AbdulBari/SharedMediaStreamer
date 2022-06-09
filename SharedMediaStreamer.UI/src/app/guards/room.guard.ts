import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, take } from 'rxjs';
import { SharedMediaStreamerApiService } from '../services/api/sharedmediastreamer-api.service';

@Injectable({
  providedIn: 'root'
})
export class RoomGuard implements CanActivate {

  constructor(
    private apiService: SharedMediaStreamerApiService,
    private router: Router
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const roomId = route.paramMap.get('roomId');

      if(!roomId) {
        this.router.navigate(['/404']);
        return false;
      }

      return this.validateRoom(roomId!, route);        
  }

  private validateRoom(roomId: string, route: ActivatedRouteSnapshot)
  {
    return new Promise<boolean>(resolve => {
      this.apiService.getRoom(roomId).pipe(take(1)).subscribe(room => {

        if (room && room.roomId === roomId)
        {
          resolve(true);
        }
        else{
          this.router.navigate(['/404']);
          resolve(false);
        }

      });
    });
  }
}
