import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../_services/index';
import { NotificationsService } from 'angular2-notifications';

@Component({
	selector: 'signin-page',
	templateUrl: './signin-page.component.html',
	styleUrls: [ './signin-page.component.css' ]
})
export class SigninPageComponent implements OnInit {
	model: any = {};
	loading = false;
	returnUrl: string;
	disablePassword: boolean = true;

	constructor(
		private route: ActivatedRoute,
		private router: Router,
		private authenticationService: AuthenticationService,
		private notificationService: NotificationsService
	) {}

	ngOnInit() {
		// reset login status
		this.authenticationService.logout();
		// get return url from route parameters or default to '/'
		this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
	}

	onChange() {
		this.authenticationService.getByLogin(this.model.userName).subscribe((result) => {
			this.disablePassword = false;
		});
	}

	login() {
		this.loading = true;
		this.authenticationService.login(this.model.userName, this.model.password).subscribe(
			(data) => {
				this.router.navigate([ this.returnUrl ]);
				this.notificationService.success('Auth success', `You logged in as ${this.model.userName}`);
			},
			(error) => {
				let description = JSON.parse(error._body).error_description;
				this.notificationService.error('Authentification Error', description);
				this.loading = false;
			}
		);
	}
}
