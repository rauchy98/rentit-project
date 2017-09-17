import { Component, OnInit, Input } from '@angular/core';
import { Order } from 'app/_models';
import { OrderService } from 'app/_services';
import { NotificationsService } from 'angular2-notifications';
import { Router } from '@angular/router';

@Component({
	selector: 'pickorderdate',
	templateUrl: './pickorderdate-page.component.html',
	styleUrls: [ './pickorderdate-page.component.css' ]
})
export class PickOrderDatePageComponent {
	@Input() productId: number;

	order: Order = new Order();
    
	constructor(
		private orderService: OrderService,
		private notificationsService: NotificationsService,
		private router: Router
    ) {}

	formSubmited() {
		this.orderService.createOrder(this.productId, this.order).subscribe(() => {
			this.notificationsService.success('Order', 'Order successfuly created. You can see orders in your cart');
		});
	}
	parseStartDate(dateString: string) {
		if (dateString) {
			this.order.start = new Date(dateString);
		}
		return null;
	}
	parseEndDate(dateString: string) {
		if (dateString) {
			this.order.end = new Date(dateString);
		}
		return null;
	}
}
