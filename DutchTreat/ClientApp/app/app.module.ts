import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ProductList } from './Shop/ProductList.component';
import { DataService } from './shared/dataService';
import { Cart } from './shop/cart.component';
import { HttpClientModule } from '@angular/common/http';

import { RouterModule } from "@angular/router";
import { Checkout } from './checkout/checkout.component';
import { Shop } from './Shop/shop.component';
import { Login } from './login/login.componenet';
import { FormsModule } from "@angular/forms";

let routes = [
    { path: "", component: Shop },
    { path: "checkout", component: Checkout },
    { path: "login", component: Login },
];

@NgModule({
  declarations: [
      AppComponent,
      ProductList,
      Cart,
      Shop,
      Checkout,
      Login
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: false // for Debugging of the routes
      }),
      FormsModule,
  ],
    providers: [
        DataService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
