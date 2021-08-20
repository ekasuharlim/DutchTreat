import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { Store } from './Services/store.service';
import { CartView } from './views/cartView.component';
import ProductListView from './views/productListView.component';

@NgModule({
  declarations: [
        AppComponent,
        ProductListView,
        CartView
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
    providers: [
        Store

    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
