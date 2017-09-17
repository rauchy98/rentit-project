import { Injectable } from '@angular/core/';

@Injectable()
export class TokenProvider {
	set token(value: string) {
		localStorage.setItem('token', value);
	}
	get token(): string {
		return localStorage.getItem('token');
	}
	removeToken() {
		localStorage.removeItem('token');
	}
}
