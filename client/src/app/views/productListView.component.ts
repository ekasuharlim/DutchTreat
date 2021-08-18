import { Component } from "@angular/core";

@Component({
    selector: "product-list",
    templateUrl: "productListView.component.html"
})
export default class ProductListView {
    public products = [{
        title: "Van gogh mug",
        price: "19"
    }, {
        title: "Vam gogh poster",
        price: "3"
    }
    ]

    public title = "a test property"
}