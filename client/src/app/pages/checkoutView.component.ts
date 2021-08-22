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
}