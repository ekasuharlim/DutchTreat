import { RouterModule } from "@angular/router";
import { CheckOut } from "../pages/checkoutView.component";
import { FrontPageStore } from "../pages/frontPageStoreView.component";

const routes = [
    { path: "", component: FrontPageStore },
    { path: "checkout", component: CheckOut}
];

const router = RouterModule.forRoot(routes);

export default router;