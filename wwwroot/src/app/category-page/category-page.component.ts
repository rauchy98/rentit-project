import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { CategoryService } from 'app/_services';

import 'rxjs/add/operator/switchMap';

@Component({
	moduleId: module.id,
	selector: 'category',
	templateUrl: './category-page.component.html',
	styleUrls: [ './category-page.component.css' ]
})

export class CategoryPageComponent implements OnInit {
	categoryId: number;

	constructor(private route: ActivatedRoute, private categoryService: CategoryService) {}

	ngOnInit() {
		this.route.params.subscribe((params: Params) => {
			this.categoryId = +params['id'];
		});
	}
}
