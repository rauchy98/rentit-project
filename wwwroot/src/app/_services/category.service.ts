import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Category, Product } from '../_models/index';
import { Server } from '../server';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class CategoryService {
	private headers = new Headers({ 'Content-Type': 'application/json' });
	private categotyUrl = Server.serverUrl + 'api/Category';

	constructor(private http: Http) {}

	getAll(): Observable<Category[]> {
		return this.http
			.get(`${this.categotyUrl}`)
			.map((response) => response.json() as Category[])
			.catch(this.handleError);
    }
    
	getProducts(categotyId: number): Observable<Product[]> {
		return this.http
			.get(`${this.categotyUrl}/${categotyId}`)
			.map((response) => response.json().products as Product[])
			.catch(this.handleError);
	}

	private handleError(error: any): Promise<any> {
		console.error('An error occurred', error);
		return Promise.reject(error.message || error);
	}
}
