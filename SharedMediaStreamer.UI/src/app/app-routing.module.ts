import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './components/homepage/homepage.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { RoomComponent } from './components/room/room.component';
import { RoomGuard } from './guards/room.guard';

const routes: Routes = [
  { path: '', component: HomepageComponent}, 
  { path: "room/:roomId", component: RoomComponent, canActivate: [RoomGuard]},
  { path: '404', component: PageNotFoundComponent},
  { path: '**', redirectTo: '/404'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
