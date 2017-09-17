import { Component, OnInit } from '@angular/core';
import { OrderService, ProductService } from 'app/_services';
import { Order } from 'app/_models';
import { Product } from 'app/_models/';

@Component({
	selector: 'orders-page',
	templateUrl: './orders-page.component.html',
	styleUrls: [ './orders-page.component.css' ]
})

export class OrdersPageComponent implements OnInit {
	orders: Order[] = [];
  products: Product[] = [];
  isLoad = true;
  
  constructor(private orderService: OrderService, private productService: ProductService) {}
  
	ngOnInit() {
		this.orderService.getOrders().subscribe((orders) => {
			orders.forEach((order) => {
				this.products.push();
				this.productService.getProduct(order.productId).subscribe((product) => {
					order.product = product;
					this.orders.push(order);
				});
			});
		});
	}
}
