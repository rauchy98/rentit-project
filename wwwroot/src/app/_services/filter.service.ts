import { Injectable } from '@angular/core/';
import { Server } from '../server';
import { Http } from '@angular/http';
import { HttpErrorHandler } from 'app/_services/http-error.handler';
import { Filter } from '../_models/filter';
import { Observable } from 'rxjs/Observable';
import { Product } from 'app/_models/';
import { FilterValue } from '../_models/filter-value';

@Injectable()
export class FilterService {
	private filterUrl = Server.serverUrl + 'api/Filter';

	constructor(private http: Http, private errorHandler: HttpErrorHandler) {}

	getFilters(categoryId: number): Observable<Filter[]> {
		return this.http
			.get(`${this.filterUrl}/Category/${categoryId}`)
			.map((response) => response.json() as Filter[])
			.catch((error) => this.errorHandler.handleError(error));
	}
	getProducts(filterValues: FilterValue[]): Observable<Product[]> {
		return this.http
			.put(this.filterUrl, filterValues)
			.map((response) => response.json() as Product[])
			.catch((error) => this.errorHandler.handleError(error));
	}
}
