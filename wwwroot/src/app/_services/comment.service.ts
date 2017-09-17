import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Comment } from '../_models/index';
import { Server } from '../server';
import { HttpErrorHandler } from 'app/_services/http-error.handler';
import { TokenProvider } from 'app/_services/token.provider';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class CommentService {
	private productUrl = Server.serverUrl + 'api/Product';

	constructor(private http: Http, private errorHandler: HttpErrorHandler, private tokenProvider: TokenProvider) {}

	getComments(productId: number): Observable<Comment[]> {
		return this.http
			.get(`${this.productUrl}/${productId}/Comments`)
			.map((response) => response.json() as Comment[])
			.catch((error) => this.errorHandler.handleError(error));
	}

	addComment(productId: number, comment: Comment): Observable<void> {
		return this.http
			.post(`${this.productUrl}/${productId}/Comments`, JSON.stringify(comment), this.getHeaders())
			.map((response) => response)
			.catch((error) => this.errorHandler.handleError(error));
  }
  
	private getHeaders() {
		// create authorization header with jwt token
		let headers = new Headers({ Authorization: this.tokenProvider.token, 'Content-Type': 'application/json' });
		return { headers: headers };
	}
}
