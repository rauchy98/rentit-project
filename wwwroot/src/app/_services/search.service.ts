import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Product } from 'app/_models/';
import { Category } from 'app/_models/category';
import { Server } from 'app/server';
import { HttpErrorHandler } from 'app/_services/http-error.handler';

@Injectable()
export class SearchService {
	private headers = new Headers({ 'Content-Type': 'application/json' });
	private searchUrl = Server.serverUrl + 'api/Search';

	constructor(private http: Http, private errorHandler: HttpErrorHandler) {}

	search(query: string) {
		return this.http
			.get(`${this.searchUrl}/${query}`)
			.map((response) => response.json() as { products: Product[]; categories: Category[] })
			.catch((error) => this.errorHandler.handleError(error));
	}
}
