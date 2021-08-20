export class OrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productTitle: string;
}

export class Order {
    orderId: number;
    orderDate: Date;
    orderNumber: string;
    items: OrderItem[];

    get subTotal(): number {
        let result = this.items.reduce(
            (total, val) => {
                return total + (val.unitPrice * val.quantity);
            },0)
        return result;
    }
}