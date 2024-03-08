export class Sediste {

    constructor(broj, zauzetost) {
        this.broj = broj;
        this.zauzetost = zauzetost;
        //true - zauzeto
        //false - slobodno
    }

    get Broj(){
        return this.broj;
    }

    set Broj(value){
        this.broj = value;
    }

    get Zauzetost() {
        return this.zauzetost;
    }

    set Zauzetost(value) {
        this.zauzetost = value;
    }
}