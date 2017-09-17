import { Product } from './product';
export class Order {
    id: number;
    start: Date;
    end: Date;
    status: OrderStatus;
    amount: number;
    price: number;
    productId: number;
    product: Product;
};

export enum OrderStatus
    {
        UnConfirmed, Confirmed, Rented
    }