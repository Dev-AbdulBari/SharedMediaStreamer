import { Component, Input, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
	selector: 'nav-contents',
	templateUrl: './navigation.component.html',
	styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

	isHandset$!: Observable<boolean>;
	@Input() title!: string;

	constructor(
		private readonly breakpointObserver: BreakpointObserver
	) { }

	ngOnInit(): void {
		this.isHandset$ = this.breakpointObserver.observe(Breakpoints.Handset)
			.pipe(map(result => result.matches));
	}

}
