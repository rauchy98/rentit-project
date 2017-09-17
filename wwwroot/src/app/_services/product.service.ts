import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Product } from '../_models';
import { Server } from '../server';
import { HttpErrorHandler } from 'app/_services/http-error.handler';
import { TokenProvider } from 'app/_services/token.provider';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class ProductService {
	private productsUrl = Server.serverUrl + 'api/Product';

	constructor(private http: Http, private errorHandler: HttpErrorHandler, private tokenProvider: TokenProvider) {}

	getProducts(categoryId: number): Observable<Product[]> {
		return this.http
			.get(`${this.productsUrl}/Category/${categoryId}`)
			.map((response) => response.json() as Product[])
			.catch((error) => this.errorHandler.handleError(error));
	}

	getProduct(id: number): Observable<Product> {
		const url = `${this.productsUrl}/${id}`;
		return this.http
			.get(url)
			.map((response) => response.json() as Product)
			.catch((error) => this.errorHandler.handleError(error));
	}

	create(categoryId: number, product: Product): Observable<Product> {
		return this.http
			.post(`${this.productsUrl}/Category/${categoryId}`, JSON.stringify(product), { headers: this.getHeaders() })
			.map((response) => response.json() as Product)
			.catch((error) => this.errorHandler.handleError(error));
	}

	update(product: Product): Observable<void> {
		return this.http
			.put(`${this.productsUrl}/${product.id}`, JSON.stringify(product), this.getHeaders())
			.map((response) => response.json())
			.catch((error) => this.errorHandler.handleError(error));
	}

	addRequest(id: number): Observable<Product> {
		const url = `${this.productsUrl}/${id}/Request`;
		return this.http
			.put(url, null, { headers: this.getHeaders() })
			.map((response) => response)
			.catch((error) => this.errorHandler.handleError(error));
	}

	checkRequest(id: number): Observable<boolean> {
		const url = `${this.productsUrl}/${id}/CheckRequest`;
		return this.http
			.get(url, { headers: this.getHeaders() })
			.map((response) => response.text() == 'true')
			.catch((error) => this.errorHandler.handleError(error));
	}

	private handleError(error: any): Promise<any> {
		return Promise.reject(error.message || error);
	}
	private getToken() {
		// create authorization header with jwt token
		return localStorage.getItem('token');
	}
	private getHeaders() {
		return new Headers({ 'Content-Type': 'application/json', Authorization: this.tokenProvider.token });
	}
}
