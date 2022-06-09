import { _resolveDirectionality } from '@angular/cdk/bidi/directionality';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { SharedMediaStreamerApiService } from 'src/app/services/api/sharedmediastreamer-api.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {

  constructor(private apiService :SharedMediaStreamerApiService, private router: Router, private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
  }

  public CreateRoom() {
    this.apiService.postRoom().pipe(take(1)).subscribe(roomId => {
      this.router.navigate(['/room/' + roomId], {relativeTo: this.currentRoute});
    });
  }

}
