import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Product } from "../shared/product";
import { Order, OrderItem } from "../shared/order";
import { LoginRequest } from "../shared/loginRequest";
import { LoginResult } from "../shared/loginResult";

@Injectable()
export class Store {

    public products: Product[] = [];
    public order: Order = new Order();
    private token = "";
    private tokenExpiry = new Date();

    constructor(private http: HttpClient) {
        this.order.orderNumber = Math.floor((Math.random() * 100) + 1).toString();
        this.order.items = [];
    }


    public loadProducts() {
        return this.http.get<[]>("/api/product").pipe(map(data => {
            this.products = data;
            return;
        }))
    }

    public login(request: LoginRequest) {
        return this.http.post<LoginResult>("/account/createtoken", request).pipe(map(data => {
            this.token = data.token;
            this.tokenExpiry = data.expiration;
            return;
        }))
    }

    public submitOrder() {
        var headers = new HttpHeaders().set("Authorization", `Bearer ${this.token}`)
        return this.http.post("/api/orders", this.order, { headers: headers}).pipe(map(data => { return; }));
    }

    public isLoginRequired() {
        return this.token.length === 0 || this.tokenExpiry < new Date();
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