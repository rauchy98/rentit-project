import { Injectable } from '@angular/core/';
import { Server } from '../server';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Category } from 'app/_models';
import { HttpErrorHandler } from './http-error.handler';
import { Characteristic } from '../_models/characteristic';

@Injectable()
export class CharactericticService {
	private headers = new Headers({ 'Content-Type': 'application/json' });
	private characteristicUrl = Server.serverUrl + 'api/Characteristic';

	constructor(private http: Http, private errorHandler: HttpErrorHandler) {}

	addCharacteristic(productId: number, filterId: number, value: string) {
		return this.http
			.post(`${this.characteristicUrl}/Product/${productId}/Filter/${filterId}`, { Value: value })
			.map((response) => response)
			.catch((response) => this.errorHandler.handleError(response));
	}

	getCharacteristic(productId: number) {
		return this.http
			.get(`${this.characteristicUrl}/Product/${productId}`)
			.map((response) => response.json() as Characteristic[])
			.catch((response) => this.errorHandler.handleError(response));
	}

	private handleError(error: any): Promise<any> {
		console.error('An error occurred', error);
		return Promise.reject(error.message || error);
	}
}
