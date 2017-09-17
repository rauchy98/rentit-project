import { Component, OnInit, Injectable } from '@angular/core';
import { Category } from 'app/_models/index';
import { CategoryService } from 'app/_services/index';
import { NotificationsService } from 'angular2-notifications';

@Component({
	moduleId: module.id,
	selector: 'main-page',
	templateUrl: './main-page.component.html',
	styleUrls: [ './main-page.component.css' ]
})
@Injectable()
export class MainPageComponent implements OnInit {
	categories: Category[];
	subcategories: Category[] = [];
	noResults = false;
	showSubcatogory = false;

	constructor(private categotyService: CategoryService, private notificationsService: NotificationsService) {}

	ngOnInit() {
		this.categotyService.getAll().subscribe((categories) => {
			this.categories = categories;
		});
	}

	getSubcategory(category: Category) {
		this.showSubcatogory = true;
		this.subcategories = category.subcategories;
	}
	hideSubcategoty() {
		this.showSubcatogory = false;
	}
}
