import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Server } from '../server';
import { Register, User } from '../_models/index';
import { HttpErrorHandler } from 'app/_services/http-error.handler';
import { TokenProvider } from 'app/_services/token.provider';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthenticationService {
	private serverUrl = Server.serverUrl;
	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private http: Http, private errorHandler: HttpErrorHandler, private tokenProvider: TokenProvider) {}

	login(username: string, password: string) {
		let loginHeaders = new Headers({ 'content-type': 'application/x-www-form-urlencoded' });
		return this.http
			.post(
				this.serverUrl + 'Token',
				//JSON.stringify({ grant_type: 'password', username: username, password: password }),
				`grant_type=password&username=${username}&password=${password}`,
				{ headers: loginHeaders }
			)
			.map((response: Response) => {
				let currentUser;
				let token = response.json();
				this.tokenProvider.token = `${token.token_type} ${token.access_token}`;
				this.http
					.get(`${this.serverUrl}api/Users/Me`, this.getHeaders())
					.map((response) => response.json())
					.catch((response) => this.errorHandler.handleError(response))
					.subscribe((user) => {
						currentUser = user;
						localStorage.setItem('user', JSON.stringify(currentUser));
					});
			})
			.catch((x) => this.errorHandler.handleError(x));
	}

	getByLogin(login: string): Observable<User> {
		return this.http
			.get(`${this.serverUrl}api/Account/CheckLogin/${login}`, this.getHeaders())
			.map((response: Response) => response.json() as User)
			.catch((error) => Promise.reject(error));
	}

	create(user: Register) {
		return this.http
			.post(this.serverUrl + 'api/Account/Register', JSON.stringify(user), { headers: this.headers })
			.map((response) => response)
			.catch((x) => this.errorHandler.handleError(x));
	}

	logout() {
		// remove user from local storage to log user out
		this.tokenProvider.removeToken();
	}
	private getHeaders() {
		// create authorization header with jwt token
		let headers = new Headers({ Authorization: this.tokenProvider.token });
		return { headers: headers };
	}
}
