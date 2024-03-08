import { formaBioskopa } from "./formaBioskopa.js";
import { Sala } from "./sala.js";

export class Bioskop {
    
    constructor(b) {

        this.id = b.id;
        this.naziv = b.naziv;
        this.brojSala = b.brojSala;
        this.brojMestaUSalama = b.brojMestaUSalama;
        this.formaBioskopaInstanca = new formaBioskopa(this, b.formaBioskopa);
        this.kontejnerBioskop = null;
        this.sale = [];

        for(let i = 0; i < this.brojSala; i++) {
            const sala = new Sala(b.sale[i].id, i+1, this.formaBioskopaInstanca, this.id, this.brojMestaUSalama)
            this.sale.push(sala);
        }
    }
    
    crtajBioskop(host) {
        if(!host) {
            throw new Error("Host element za 'Bioskop' ne postoji!");
        }

        let divNazivaBioskopa = document.createElement("div");
        divNazivaBioskopa.className = "divNazivaBioskopa";
        
        let nazivBioskopa = document.createElement("label");
        nazivBioskopa.className = "nazivBioskopa";
        nazivBioskopa.innerHTML = this.naziv;

        divNazivaBioskopa.appendChild(nazivBioskopa);

        this.kontejnerBioskop = document.createElement("div");
        this.kontejnerBioskop.className = "kontejnerBioskop";
        this.kontejnerBioskop.appendChild(divNazivaBioskopa);

        host.appendChild(this.kontejnerBioskop);

        let divMiniKontejner = document.createElement("div");
        divMiniKontejner.className = "divMiniKontejner";
        this.kontejnerBioskop.appendChild(divMiniKontejner);

        this.formaBioskopaInstanca.crtajFormuBioskopa(divMiniKontejner);

        const kontejnerGrupeSala_big = document.createElement("div");
        kontejnerGrupeSala_big.className = "kontejnerGrupeSala_big";
        divMiniKontejner.appendChild(kontejnerGrupeSala_big);        

        this.sale.forEach((SalaEL) => {
            SalaEL.crtajSalu(kontejnerGrupeSala_big);
        });
    }

    dodajFilmSali(){
        this.sale.forEach((SalaEL) => {
            SalaEL.addStavkaFilm();
        });
    }

    ukloniFilmSali(index){
        this.sale.forEach((SalaEL) => {
            SalaEL.removeStavkaFilm(index);
        });
    }

    azurirajFilmSali(index, stavka){
        this.sale.forEach((SalaEL) => {
            SalaEL.updateStavkaFilm(index, stavka);
        });
    }
}