import { Component, OnInit } from '@angular/core';
import { CategoryService, ProductService, FilterService } from 'app/_services';
import { Category, Product, Filter } from 'app/_models';
import { NotificationsService } from 'angular2-notifications';
import { Router } from '@angular/router/';
import { AvailableStatus } from '../_models/product';
import { Characteristic } from 'app/_models/characteristic';
import { CharactericticService } from 'app/_services/characteristic.service';

@Component({
	moduleId: module.id,
	selector: 'add-product-page',
	templateUrl: './add-product-page.component.html',
	styleUrls: [ './add-product-page.component.css' ]
})

export class AddProductPageComponent implements OnInit {
	pictures: string[] = [];
	model: Product = new Product();
	picture: string = '';
	categories: Category[];
	subcategories: Category[];
	subcategory: Category;
	filters: any[];
  AvailableStatus: typeof AvailableStatus = AvailableStatus;
  
	constructor(
		private categoryService: CategoryService,
		private productService: ProductService,
		private notificationsService: NotificationsService,
		private filterService: FilterService,
		private charactericticService: CharactericticService,
		private router: Router
	) {}

	ngOnInit() {
		this.categoryService.getAll().subscribe((categories) => (this.categories = categories));
	}

	categoryChange(category) {
		this.subcategories = category.subcategories;
	}

	subcategoryChange(subcategory) {
		this.subcategory = subcategory;
		this.filterService.getFilters(subcategory.id).subscribe((filters) => {
			this.filters = filters;
		});
	}
	formSubmited() {
		if (!this.pictures.length) return;
		this.model.picture = this.pictures;
		this.productService.create(this.subcategory.id, this.model).subscribe((product) => {
			this.filters.forEach((filter) => {
				this.charactericticService.addCharacteristic(product.id, filter.id, filter.value).subscribe((x) => x);
			});
			this.router.navigate([ `product/${product.id}` ]);
			this.notificationsService.success('Done', 'Product successfully added');
		});
	}

	addPhoto() {
		this.pictures.push(this.picture);
		this.picture = '';
	}

	private isImageExtention(pictureUrl: string): boolean {
		var extentions = [ '.jpg', '.jpeg', '.png' ];
		let result = true;
		if (pictureUrl.length < 5) return false;

		for (let i = 0; i < extentions.length; i++) {
			if (pictureUrl && pictureUrl.lastIndexOf(extentions[i]) + extentions[i].length == pictureUrl.length) {
				return true;
			}
		}
		return false;
	}

	imageError() {
		this.pictures.pop();
	}
}
