import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { TokenProvider } from 'app/_services/token.provider';

@Injectable()
export class AuthGuard implements CanActivate {
	constructor(private router: Router, private tokenProvider: TokenProvider) {}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		if (this.tokenProvider.token) {
			return true;
		}

		// not logged in so redirect to login page with the return url
		this.router.navigate([ '/signin' ], { queryParams: { returnUrl: state.url } });
		return false;
	}
	checkLogin(): Boolean {
		if (!this.tokenProvider.token) {
			return true;
		}
		return false;
	}
}
