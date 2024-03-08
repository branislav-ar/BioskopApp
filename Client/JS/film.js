export class Film {

    constructor(id, naziv, cenaKarte) {
        this.id = id;
        this.naziv = naziv;
        this.cenaKarte = cenaKarte;
    }

    get Naziv(){
        return this.naziv;
    }
    set Naziv(value){
        this.naziv = value;
    }

    get CenaKarte(){
        return this.cenaKarte;
    }
    set CenaKarte(value){
        this.cenaKarte = value;
    }
}