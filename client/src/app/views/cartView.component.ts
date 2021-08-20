import { Component, Injectable } from "@angular/core";
import { Store } from "../Services/store.service";
import { Order, OrderItem } from "../shared/order";
import { Product } from "../shared/product";

@Component({
    selector: "cart",
    templateUrl: "cartView.component.html"
})
export class CartView {

    

    constructor(public store: Store) {
    }

}