import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { CategoryService, SearchService } from 'app/_services';
import 'rxjs/add/operator/switchMap';

@Component({
	moduleId: module.id,
	selector: 'search-page',
	templateUrl: './search-page.component.html',
	styleUrls: [ './search-page.component.css' ]
})
export class SearchPageComponent implements OnInit {
	query: string;

	constructor(
		private location: Location,
		private route: ActivatedRoute,
		private categoryService: CategoryService,
		private searchService: SearchService
	) {}

	ngOnInit() {
		this.route.params.subscribe((params: Params) => {
			this.query = params['q'];
		});
	}
}
