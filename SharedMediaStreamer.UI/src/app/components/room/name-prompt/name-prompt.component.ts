import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-name-prompt',
  templateUrl: './name-prompt.component.html',
  styleUrls: ['./name-prompt.component.scss']
})
export class NamePromptComponent implements OnInit {

  username: string = "";

  constructor() { }

  ngOnInit(): void {
  }

}
