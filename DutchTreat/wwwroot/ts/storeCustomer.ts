class storeCustomer {

    constructor(private firstname: string, private lastname: string) {
    }

    public visits: number = 0;
    private ourName: string;

    public showName() {
        alert(this.firstname + " " + this.lastname);
        return true;
    }

    set name(val) {
        this.name = val;
    }

    get name() {
        return this.ourName;
    }
}
