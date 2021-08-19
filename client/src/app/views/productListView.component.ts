import { Component } from "@angular/core";
import { Store } from "../Services/store.service";

@Component({
    selector: "product-list",
    templateUrl: "productListView.component.html"
})
export default class ProductListView {

    public products;
    constructor(private store: Store) {
        this.products = store.products;
        console.log("IN");
        console.log(this.products);
    }

    public title = "a test property"
}