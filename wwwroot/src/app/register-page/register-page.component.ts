import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services/index';
import { Register } from '../_models/index';
import { NotificationsService } from 'angular2-notifications';

@Component({
	moduleId: module.id,
	templateUrl: 'register-page.component.html'
})
export class RegisterPageComponent implements OnInit {
	ngOnInit() {
		this.authenticationService.logout();
	}

	model: any = {};

	constructor(
		private router: Router,
		private authenticationService: AuthenticationService,
		private notificationsService: NotificationsService
	) {}

	register() {
		this.authenticationService.create(this.model).subscribe((data) => {
			this.authenticationService.login(this.model.UserName, this.model.Password);
			this.router.navigate([ '/signin' ]);
			this.notificationsService.success(
				'Registration',
				`You are successfully registered as ${this.model.UserName}`
			);
		});
	}
	checkPass() {
		if (this.model.Password === this.model.ConfirmPassword && this.model.Password.length >= 6) return true;
		return false;
	}
	validateEmail() {
		var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
		return re.test(this.model.Email);
	}
}
