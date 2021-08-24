import { templateJitUrl } from "@angular/compiler";
import { Component } from "@angular/core";
import { Store } from "../Services/store.service";

@Component({
    selector: "checkout",
    templateUrl: "checkoutView.component.html"
})
export class CheckOut {

    constructor(public store: Store) {

    }

    public submit() {
        this.store.submitOrder().subscribe(() => {
            console.log("order created");
        }, error => {
            console.log("order creation failed");
            console.log(error);
        });
    }
}