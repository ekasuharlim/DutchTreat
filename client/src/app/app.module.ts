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
import { AuthActivator } from './Services/authActivator.service';
import { FormsModule } from '@angular/forms';
import { LoginPage } from './pages/LoginPageView.component';

@NgModule({
  declarations: [
        AppComponent,
        ProductListView,
        CartView,
        FrontPageStore,
        CheckOut,
        LoginPage
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule,
      router,
      FormsModule
  ],
    providers: [
        Store,
        AuthActivator        
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
