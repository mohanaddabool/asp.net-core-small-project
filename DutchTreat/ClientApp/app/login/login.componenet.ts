import { Component } from "@angular/core";
import { DataService } from "../shared/dataService";
import { Router } from "@angular/router";

@Component({
    selector: "login",
    templateUrl: "login.component.html",
    styleUrls: []
})
export class Login {
    constructor(private data: DataService, private router: Router) {
    }

    public creds = {
        username: "",
        password: ""
    };

    onLogin() {
        this.data.login(this.data)
            .subscribe(success => {

            }, err =>
                    this.errorMessage = "Failed to login");
    }
}