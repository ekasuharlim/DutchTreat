import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../Services/store.service";
import { LoginRequest } from "../shared/loginRequest";

@Component({
    selector: "login-page",
    templateUrl: "LoginPageView.component.html"
}
)
export class LoginPage {

    constructor(private store: Store, private router: Router) {
    }

    public creds : LoginRequest = {    
        username: "",
        password: ""
    }

    public onLogin() {
        this.store.login(this.creds).subscribe(() => {
            console.log("login success");
            if (this.store.order.items.length > 0) {
                this.router.navigate(["checkout"]);
            } else {
                this.router.navigate([""]);
            }
        }, error => {
            console.log(error);
        })
    }
}