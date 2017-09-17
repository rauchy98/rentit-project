import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ProductService, UserService } from '../_services/index';
import { Product, User, AvailableStatus } from '../_models/index';
import { NotificationsService } from 'angular2-notifications';
import { CharactericticService } from 'app/_services/characteristic.service';
import { Characteristic } from 'app/_models/characteristic';
import 'rxjs/add/operator/switchMap';

@Component({
	moduleId: module.id,
	selector: 'product-page',
	templateUrl: './product-page.component.html',
	styleUrls: [ './product-page.component.css' ]
})
export class ProductPageComponent implements OnInit, OnChanges {
	characteristics: Characteristic[];
	owner: User;
	product: Product;
	isChecked: boolean = false;
	AvailableStatus: typeof AvailableStatus = AvailableStatus;

	constructor(
		private productService: ProductService,
		private userService: UserService,
		private route: ActivatedRoute,
		private notificationsService: NotificationsService,
		private charactericticService: CharactericticService,
	) {}

	ngOnInit() {
		this.route.params
			.switchMap((params: Params) => this.productService.getProduct(+params['id']))
			.subscribe((product) => {
				this.userService.getById(product.sellerId).subscribe((user) => {
					this.product = product;
					this.owner = user;
				});
				this.charactericticService.getCharacteristic(product.id).subscribe((characteristics) => {
					this.characteristics = characteristics;
					this.checkRequest(product.id);
				});
			});
	}

	ngOnChanges(changes: SimpleChanges): void {
		this.ngOnInit();
	}

	check() {
		if(this.isChecked) {
			return;
		}
		this.productService.addRequest(this.product.id).subscribe(() => {
			this.notificationsService.success('Successfully', `You add request to this this product`);
			this.isChecked = true;
			this.product.requestCount++;
		});
	}

	checkRequest(productId: number) {
		this.productService.checkRequest(productId).subscribe((x) => {
			console.log(x);
			this.isChecked = x;
		});
	}
}
