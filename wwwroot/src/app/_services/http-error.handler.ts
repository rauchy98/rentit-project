import { NotificationsService } from 'angular2-notifications';
import { Injectable, ErrorHandler } from '@angular/core/';
import { Router } from '@angular/router/';
import { TokenProvider } from 'app/_services/token.provider';

@Injectable()
export class HttpErrorHandler {
	constructor(
		private notificationsService: NotificationsService,
		private router: Router,
		private tokenProvider: TokenProvider
	) {}

	handleError(error: Response): Promise<any> {
		if (!error.ok) {
			if (error.status) {
				if (error.status == 401) {
					this.notificationsService.warn('Unauthorized', 'Please sing in');
					this.router.navigate([ '/signin' ]);
					this.tokenProvider.removeToken();
					this.router.navigate([ '/signin' ], { queryParams: { returnUrl: this.router.url } });
				} else if (error.status == 400) {
					this.notificationsService.warn('Bad Request', 'No valid input data');
				} else {
					this.notificationsService.error(
						'Server error',
						`Some error occupied in server (${error.statusText.length
							? error.statusText
							: 'Can`t connect to server'})`
					);
				}
			}
		}
		return Promise.reject(error);
	}
}
