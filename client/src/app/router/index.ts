import { RouterModule } from "@angular/router";
import { CheckOut } from "../pages/checkoutView.component";
import { FrontPageStore } from "../pages/frontPageStoreView.component";
import { LoginPage } from "../pages/LoginPageView.component";
import { AuthActivator } from "../Services/authActivator.service";

const routes = [
    { path: "", component: FrontPageStore },
    { path: "checkout", component: CheckOut, canActivate: [AuthActivator] },
    { path: "login", component: LoginPage }
];

const router = RouterModule.forRoot(routes);

export default router;