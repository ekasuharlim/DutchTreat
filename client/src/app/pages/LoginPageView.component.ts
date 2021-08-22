import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../Services/store.service";

@Component({
    selector: "login-page",
    templateUrl: "LoginPageView.component.html"
}
)
export class LoginPage {

    constructor(private store: Store, private router: Router) {
    }

    public creds =  {
        username: "",
        password: ""
    }

    public onLogin() {
        alert("logging in..");
    }
}