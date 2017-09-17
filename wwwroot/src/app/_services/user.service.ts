import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Register } from '../_models/index';
import { Server } from '../server';
import { Observable } from 'rxjs/Observable';
import { User } from '../_models/user';
import { HttpErrorHandler } from 'app/_services/http-error.handler';
import { TokenProvider } from 'app/_services/token.provider';

@Injectable()
export class UserService {
	private serverUrl = Server.serverUrl;
	constructor(private http: Http, private errorHandler: HttpErrorHandler, private tokenProvider: TokenProvider) {}

	getAll() {
		return this.http
			.get('api/Users/', this.getHeaders())
			.map((response: Response) => response.json())
			.catch((error) => this.errorHandler.handleError(error));
	}

	getById(id: number): Observable<User> {
		return this.http
			.get(`${this.serverUrl}api/Users/${id}`, this.getHeaders())
			.map((response: Response) => response.json() as User)
			.catch((error) => this.errorHandler.handleError(error));
	}

	delete(id: number) {
		return this.http
			.delete('/api/Users/' + id, this.getHeaders())
			.map((response: Response) => response.json())
			.catch((error) => this.errorHandler.handleError(error));
	}
	private getHeaders() {
		// create authorization header with jwt token
		let headers = new Headers({ Authorization: this.tokenProvider.token });
		return { headers: headers };
	}
}
