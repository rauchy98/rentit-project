import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommentService, UserService } from 'app/_services';
import { Comment, User } from '../_models/index';
import { Filter } from '../_models/filter';
import { FilterService } from '../_services/filter.service';
import { Product } from 'app/_models/product';
import { FilterValue } from 'app/_models/filter-value';

@Component({
	moduleId: module.id,
	selector: 'filters-list',
	templateUrl: './filters-list.component.html',
	styleUrls: [ './filters-list.component.css' ]
})
export class FilterListComponent implements OnChanges, OnInit {
  
	constructor(private filterService: FilterService) {}

	@Input() categoryId: number;
	filters: Filter[];
	products: Product[] = [];
	filterValues: FilterValue[] = [];

	ngOnInit(): void {
		this.filterService.getFilters(this.categoryId).subscribe((response) => (this.filters = response));
	}

	ngOnChanges(changes: SimpleChanges): void {
		if (this.categoryId) {
			this.filterService.getFilters(this.categoryId).subscribe((response) => (this.filters = response));
		}
	}

	itemChecked(event, id, value) {
		if (event.target.checked) {
			let a = { filterId: id, value: value };
			this.filterValues.push(a);
			this.filterService.getProducts(this.filterValues).subscribe((x) => (this.products = x));
		}
	}
}
