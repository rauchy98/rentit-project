import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ProductService, SearchService } from '../_services/index';
import { Product, AvailableStatus } from '../_models/index';

@Component({
	moduleId: module.id,
	selector: 'product-card',

	templateUrl: './product-card.component.html',
	styleUrls: [ './product-card.component.css' ]
})
export class ProductCardComponent implements OnInit, OnChanges {
	@Input() query: string;
	@Input() categoryId: number;
	products: Product[] = [];
	isChecked: boolean = false;
	AvailableStatus: typeof AvailableStatus = AvailableStatus;

	constructor(private productService: ProductService, private searchService: SearchService) {}

	ngOnInit() {
		if (this.categoryId) {
			this.productService.getProducts(this.categoryId).subscribe((products) => {
				this.products = products;
				this.products.forEach(x => this.checkRequest(x.id))
			});
		}
		if (this.query) {
			this.searchService.search(this.query).subscribe((x) => {
				this.products = x.products;
			});
		}
	}

	ngOnChanges(changes: SimpleChanges): void {
		if (this.categoryId) {
			this.productService.getProducts(this.categoryId).subscribe((products) => {
				this.products = products;
			});
		}
	}

	checkRequest(productId: number) {
		this.productService.checkRequest(productId).subscribe((x) => {
			console.log(x);
			this.isChecked = x;
		});
}
}
