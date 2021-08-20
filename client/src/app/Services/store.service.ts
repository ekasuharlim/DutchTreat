import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Product } from "../shared/product";
import { Order, OrderItem } from "../shared/order";

@Injectable()
export class Store {

    public products: Product[] = [];
    public order: Order = new Order();

    constructor(private http: HttpClient) {
        this.order.orderNumber = "12345";
        this.order.items = [];
    }


    public loadProducts() {
        return this.http.get<[]>("/api/product").pipe(map(data => {
            this.products = data;
            return;
        }))
    }

    public addItemToOrder(product: Product) {
        console.log("adding item");
        let item = this.order.items.find(oi => oi.productId === product.id);
        if (item) {
            item.quantity++;
        } else {
            item = new OrderItem();
            item.productId = product.id;
            item.productTitle = product.title;
            item.quantity = 1;
            item.unitPrice = product.price;
            this.order.items.push(item);
        }

        
    }
}