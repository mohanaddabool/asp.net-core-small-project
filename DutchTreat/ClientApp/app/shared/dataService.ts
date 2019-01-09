//import { HttpClient } from "selenium-webdriver/http";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { from, throwError, Observable } from 'rxjs';
import { map, filter, catchError, mergeMap } from 'rxjs/operators';
import { Product } from "./product";
import { Order, OrderItem } from "./order";
import { ProductList } from "../Shop/ProductList.component";
import { Data } from "@angular/router";


@Injectable()
export class DataService {

    constructor(private http: HttpClient) {

    }

    private token: string = "";
    private tokenExpiration: Data;

    public order: Order = new Order();

    public products: Product[] = [];

    loadProducts(): Observable<boolean> {
        return this.http.get("/api/products")
            .pipe(
                map((data: any[]) => {
                    this.products = data;
                    return true;
                }), catchError(error => {
                    return throwError('Something went wrong!')
                }));
    }

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }

    login(creds): Observable<boolean> {
        return this.http
            .post("/account/createToken", creds)
            .pipe(map((data: any) =>
            {
                this.token = data.token;
                this.tokenExpiration = data.expiration;
                return true;
            }
        )
    };

    public addToOrder(product: Product) {

        let item: OrderItem = this.order.items.find(i => i.productId == product.id);

        if (item) {

            item.quantity++;
        }
        else {
            item = new OrderItem();
            item.productId = product.id;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productArtId = product.artId;
            item.productTitle = product.title;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1;
            this.order.items.push(item);
        }
    }
}