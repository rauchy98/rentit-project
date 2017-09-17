import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { AuthGuard } from 'app/_guards';
import { AuthenticationService } from 'app/_services';

@Component({
	moduleId: module.id,
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: [ './app.component.css' ]
})
export class AppComponent {
	condition = true;
	isLogin: Boolean;
	public options = {
		timeOut: 3000
	};

	constructor(
		location: Location,
		private router: Router,
		authGuard: AuthGuard,
		private authService: AuthenticationService,
		private notificationService: NotificationsService
	) {
		router.events.forEach((event) => {
			this.isLogin = authGuard.checkLogin();
			if (location.prepareExternalUrl(location.path()) === '/main') {
				this.condition = false;
			} else {
				this.condition = true;
			}
		});
	}

	logout() {
		this.authService.logout();
		this.notificationService.info('Authorization', 'You are logged out');
	}
}
