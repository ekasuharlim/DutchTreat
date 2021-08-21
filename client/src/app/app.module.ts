import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { Store } from './Services/store.service';
import { CartView } from './views/cartView.component';
import { CheckOut } from './pages/checkoutView.component';
import { FrontPageStore } from './pages/frontPageStoreView.component';
import ProductListView from './views/productListView.component';
import router from './router';

@NgModule({
  declarations: [
        AppComponent,
        ProductListView,
        CartView,
        FrontPageStore,
        CheckOut
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule,
      router
  ],
    providers: [
        Store
        
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
