import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { HomepageComponent } from './components/homepage/homepage.component';
import { HttpClientModule } from '@angular/common/http';
import { RoomComponent } from './components/room/room.component';
import { NavigationComponent } from './navigation/navigation.component';
import { HeaderComponent } from './navigation/header/header.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { RoomGuard } from './guards/room.guard';
import { MatInputModule } from '@angular/material/input';
import { NamePromptComponent } from './components/room/name-prompt/name-prompt.component';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    HeaderComponent,
    HomepageComponent,
    RoomComponent,
    PageNotFoundComponent,
    NamePromptComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatSidenavModule,
    HttpClientModule,
    MatInputModule,
    MatDialogModule,
    FormsModule,
    MatBadgeModule,
    MatTooltipModule
  ],
  providers: [
    RoomGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
