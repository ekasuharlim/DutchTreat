import { Injectable } from "@angular/core";

@Injectable()
export class Store {

    public products = [{
        title: "Van gogh mug",
        price: "19"
    }, {
        title: "Vam gogh poster",
        price: "3"
    }
    ];
}