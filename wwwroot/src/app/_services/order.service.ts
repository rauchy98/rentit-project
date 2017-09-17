import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Order } from '../_models/index';
import { Server } from '../server';
import { HttpErrorHandler } from 'app/_services/http-error.handler';
import { TokenProvider } from 'app/_services/token.provider';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class OrderService {
	private headers = new Headers({ 'Content-Type': 'application/json' });
	private ordersUrl = Server.serverUrl + 'api/Order';

	constructor(private http: Http, private errorHandler: HttpErrorHandler, private tokenProvider: TokenProvider) {}

	getOrders(): Observable<Order[]> {
		return this.http
			.get(this.ordersUrl, this.getHeaders())
			.map((response) => response.json() as Order[])
			.catch((error) => this.errorHandler.handleError(error));
	}

	createOrder(productId: number, order: Order): Observable<void> {
		return this.http
			.post(`${this.ordersUrl}/Product/${productId}`, order, this.getHeaders())
			.map((response) => response)
			.catch((error) => this.errorHandler.handleError(error));
	}

	deleteOrder(orderId: number) {
		return this.http
			.delete(`${this.ordersUrl}/${orderId}`, this.getHeaders())
			.map((response) => response)
			.catch((error) => this.errorHandler.handleError(error));
	}

	updateOrder(order: Order) {
		return this.http
			.put(`${this.ordersUrl}/${order.id}`, order, this.getHeaders())
			.map((response) => response.json())
			.catch((error) => this.errorHandler.handleError(error));
	}
	private getHeaders() {
		// create authorization header with jwt token
		let headers = new Headers({ Authorization: this.tokenProvider.token });
		return { headers: headers };
	}
}
