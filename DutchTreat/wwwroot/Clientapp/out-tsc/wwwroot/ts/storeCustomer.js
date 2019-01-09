var storeCustomer = /** @class */ (function () {
    function storeCustomer(firstname, lastname) {
        this.firstname = firstname;
        this.lastname = lastname;
        this.visits = 0;
    }
    storeCustomer.prototype.showName = function () {
        alert(this.firstname + " " + this.lastname);
        return true;
    };
    Object.defineProperty(storeCustomer.prototype, "name", {
        get: function () {
            return this.ourName;
        },
        set: function (val) {
            this.name = val;
        },
        enumerable: true,
        configurable: true
    });
    return storeCustomer;
}());
//# sourceMappingURL=storeCustomer.js.map