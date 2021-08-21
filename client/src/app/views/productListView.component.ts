import { Component, OnInit } from "@angular/core";
import { Store } from "../Services/store.service";

@Component({
    selector: "product-list",
    templateUrl: "productListView.component.html"
})
export default class ProductListView implements OnInit {

    constructor(public store: Store) {

    }

    ngOnInit(): void {
        this.store.loadProducts().subscribe();
        console.log("in");
        console.log(this.store.products.length);
    }

    public title = "a test property"
}