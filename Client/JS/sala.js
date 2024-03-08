import { Film } from "./film.js";
import { Sediste } from "./sediste.js";

export class Sala {


    constructor(id, broj, formaBioskopaInstancaRef, idBioskopa, brojMesta){
        //id sale,      broj sale u nizu,   naziv bio,     referenca instance forme,  id bioskopa,    broj mesta 
        this.id = id;
        this.broj = broj;
        this.idBioskopa = idBioskopa;
        this.kontejnerSala = null;
        this.filmoviNiz = [];
        //this.sedistaNiz = [];
        this.formaBioskopaInstancaRef = formaBioskopaInstancaRef;
    }

    addStavkaFilm() {
        let f = document.querySelector(".filmSelect" + this.idBioskopa + "-" + this.broj);
        let stavka = document.createElement("option");

        let naziv = document.querySelector(".nazivFilmaInput" + this.idBioskopa);
        let cena = document.querySelector(".cenaFilmaInput" + this.idBioskopa);

        stavka.value = cena.value;
        stavka.name = naziv.value;
        stavka.innerHTML = naziv.value;
        stavka.className = "stavkaMeni";
        f.appendChild(stavka);
    }

    removeStavkaFilm(index) {
        let f = document.querySelector(".filmSelect" + this.idBioskopa + "-" + this.broj);
        f.options[index] = null;
        delete f.options[index];
    }

    updateStavkaFilm(index, stavka) {
        let f = document.querySelector(".filmSelect" + this.idBioskopa + "-" + this.broj);
        f.options[index].name = stavka.name;
        f.options[index].value = stavka.value;
        f.options[index].innerHTML = stavka.innerHTML;
    }

    dodajFilm(film){

        const dodajFilmOvde = document.querySelector(".labelTekst" + this.idBioskopa + "-" + this.broj);
        let btnDodaj = document.querySelector(".btnRezervisi" + this.idBioskopa + "-" + this.broj);
        btnDodaj.setAttribute('disabled', true);

        this.filmoviNiz.push(film);
        console.log(this.filmoviNiz);
        let t = document.querySelector(".labelTekst2" + this.idBioskopa + "-" + this.broj);
        t.innerHTML = "";

        const labelTekst2 = document.createElement("li");
        labelTekst2.className = "labelTekst" + this.idBioskopa + "-" + this.broj;
        labelTekst2.innerHTML = `🎥: ${film.naziv}, 🏷️: ${film.cenaKarte}RSD`;
        console.log(`Karta za 🎥: ${film.naziv} je rezervisana! ✅`);

        dodajFilmOvde.appendChild(labelTekst2);
    }

    ukloniFilm(film){
        let t = document.querySelector(".labelTekst2" + this.idBioskopa + "-" + this.broj);
        t.innerHTML = "- nema rezervacija -";

        let t2 = document.querySelector(".labelTekst" + this.idBioskopa + "-" + this.broj);
        t2.innerHTML = "";

        alert(`Rezervacija za Salu ${this.broj} je poništena! ❌`);
        delete this.filmoviNiz;
        this.filmoviNiz = [];
    }

    zavrsiRezervaciju(){
        let t = document.querySelector(".labelTekst2" + this.idBioskopa + "-" + this.broj);
        t.innerHTML = "- nema rezervacija -";
        let t2 = document.querySelector(".labelTekst" + this.idBioskopa + "-" + this.broj);
        t2.innerHTML = "";

        let suma = 0;
        let str = `RAČUN ZA SALU ${this.broj}: \n\n`;
        this.filmoviNiz.forEach((film) => {
            suma += parseInt(film.cenaKarte);
            str += `🎥: ${film.naziv}, 🏷️: ${film.cenaKarte}RSD\n`;
        });
        str += "\nUKUPNO: " + suma + "RSD";
        alert(str);
        
        delete this.filmoviNiz;
        this.filmoviNiz = [];
    }

    crtajSalu(host) {
        if(!host) {
            throw new Error("Host element za 'Sala' ne postoji!");
        }
        
        //GLAVNI KONTEJNER deo na 'kontejnerGrupeSala' kao host
        this.kontejnerSala = document.createElement("div");
        this.kontejnerSala.className = "kontejnerSala";
        host.appendChild(this.kontejnerSala);
        
        let kontejnerSalaNaziv = document.createElement("div");
        kontejnerSalaNaziv.className = "kontejnerSalaNaziv";
        this.kontejnerSala.appendChild(kontejnerSalaNaziv);

        //NAZIV sale na 'kontejnerSala'
        const naziv = document.createElement("label");
        naziv.className = "kontejnerSalaNaziv_lab";
        naziv.innerHTML = `Sala ${this.broj}`;
        kontejnerSalaNaziv.appendChild(naziv);

        //SELECT-OPTION deo
        //'formaSale' se dodaje na 'kontejnerSala'
        const formaSale = document.createElement("form");
        formaSale.className = "formaSale";
        this.kontejnerSala.appendChild(formaSale);

        //DIV za labele u formaSale
        const formaSalelabDiv = document.createElement("div");
        formaSalelabDiv.className = "formaSalelabDiv";
        formaSale.appendChild(formaSalelabDiv);

        //DIV za input u formaSale
        const formaSaleinputDiv = document.createElement("div");
        formaSaleinputDiv.className = "formaSaleinputDiv";
        formaSale.appendChild(formaSaleinputDiv);

        //SELLECT-OPTION 1
        //labela
        const labelaSelect1 = document.createElement("label");
        labelaSelect1.className = "selectLabela";
        labelaSelect1.innerHTML = "IZABERITE FILM: ";
        formaSalelabDiv.appendChild(labelaSelect1);

        //select-option
        const sel1 = document.createElement("select");
        //let id = "filmSelect" + this.idBioskopa + "-" + this.broj;
        sel1.className = "filmSelect" + this.idBioskopa + "-" + this.broj;
        sel1.classList.add("selectInput");
        formaSaleinputDiv.appendChild(sel1);

        let stavka;
        this.formaBioskopaInstancaRef.stavke.forEach(filmEL => {
            stavka = document.createElement("option");
            stavka.className = "stavkaUSali";

            stavka.value = filmEL.cenaKarte;
            stavka.name = filmEL.naziv;
            stavka.innerHTML = filmEL.naziv;
            sel1.add(stavka);
        });

        sel1.onchange=(event) => {
            let i = sel1.selectedIndex;            
            if(i >= 0) {
                let naz = document.querySelector(".nazivFilmaInput" + this.formaBioskopaInstancaRef.id);
                naz.value = sel1.options[i].name;
                
                let cena = document.querySelector(".cenaFilmaInput" + this.formaBioskopaInstancaRef.id);
                cena.value = sel1.options[i].value;

                console.log(`Selektovana opcija: 🎥: ${naz.value}, 🏷️: ${cena.value}RSD`);
            }
        }

        sel1.onclick=(event) => {
            let identity = ".btnRezervisi" + this.idBioskopa + "-" + this.broj;
            btn1 = document.querySelector(identity);
            if(sel1.selectedIndex >= 0){
                btn1.removeAttribute("disabled");
            }
            else{
                btn1.setAttribute('disabled', true);
            }
        }

        //DIV info
        const infoDiv = document.createElement("div");
        infoDiv.className = "infoDiv";
        this.kontejnerSala.appendChild(infoDiv);

        //labela 1
        let labelTekst = document.createElement("label");
        labelTekst.className = "labelTekst";
        labelTekst.innerHTML = "TRENUTNO STANJE:";
        infoDiv.appendChild(labelTekst);

        //labela 2
        labelTekst = document.createElement("label");
        labelTekst.className = "labelTekst2" + this.idBioskopa + "-" + this.broj;
        labelTekst.innerHTML = "- nema rezervacija -";
        infoDiv.appendChild(labelTekst);

        
        //kontejner info
        const kontejnerInfo = document.createElement("ul");
        kontejnerInfo.className = "kontejnerLegenda";
        infoDiv.appendChild(kontejnerInfo);

        const labelTekst2 = document.createElement("li");
        labelTekst2.className = "labelTekst" + this.idBioskopa + "-" + this.broj;
        labelTekst2.innerHTML = "";
        kontejnerInfo.appendChild(labelTekst2);

        //DIV dugmeta
        const btnDiv = document.createElement("div");
        btnDiv.className = "divDugmetaSala";
        this.kontejnerSala.appendChild(btnDiv);

        const btnDiv2 = document.createElement("div");
        btnDiv2.className = "divDugmetaSala";
        this.kontejnerSala.appendChild(btnDiv2);

        //btnRazervisi
        let btn1 = document.createElement("button");
        btn1.type = "button";
        btn1.setAttribute('disabled', true);
        btn1.innerHTML = "REZERVIŠI KARTU";
        btn1.classList.add("btnRezervisi" + this.idBioskopa + "-" + this.broj);
        btn1.classList.add("dugme");

        btn1.onclick = (event) =>{

            let s = document.querySelector(".filmSelect" + this.idBioskopa + "-" + this.broj);
            //let filmid = this.formaBioskopaInstancaRef.stavke[s.selectedIndex].id;
            this.dodajFilm(this.formaBioskopaInstancaRef.stavke[s.selectedIndex]);

            let identity = ".btnRazervisiFinal" + this.idBioskopa + "-" + this.broj;
            let btnToActivate = document.querySelector(identity);
            btnToActivate.removeAttribute("disabled");

            identity = ".btnObrisiRez" + this.idBioskopa + "-" + this.broj;
            btnToActivate = document.querySelector(identity);
            btnToActivate.removeAttribute("disabled");
        }
                
        btnDiv.appendChild(btn1);
        
        //btnObrisiRez
        let btn3 = document.createElement("button");
        btn3.innerHTML = "OBRIŠI REZERVACIJU";
        btn3.type = "button";
        btn3.setAttribute('disabled', true);
        btn3.classList.add("btnObrisiRez" + this.idBioskopa + "-" + this.broj);
        btn3.classList.add("dugme");

        btn3.onclick = (event) =>{
            let s = document.querySelector(".filmSelect" + this.idBioskopa + "-" + this.broj);
            //let filmid = this.formaBioskopaInstancaRef.stavke[s.selectedIndex].id;
            this.ukloniFilm(this.formaBioskopaInstancaRef.stavke[s.selectedIndex]);

            btn3.setAttribute('disabled', true);
            let btn = document.querySelector(".btnRazervisiFinal" + this.idBioskopa + "-" + this.broj);
            btn.setAttribute('disabled', true);


            console.log("Rezervacija poništena! ❌");
        }
        btnDiv.appendChild(btn3);


        //btnRazervisiFinal
        let btn2 = document.createElement("button");
        btn2.innerHTML = "ZAVRŠI REZERVACIJU";
        btn2.type = "button";
        btn2.setAttribute('disabled', true);
        btn2.classList.add("btnRazervisiFinal" + this.idBioskopa + "-" + this.broj);
        btn2.classList.add("dugme");

        btn2.onclick = (event) =>{

            this.zavrsiRezervaciju();
            btn2.setAttribute('disabled', true);
            let btn = document.querySelector(".btnObrisiRez" + this.idBioskopa + "-" + this.broj);
            btn.setAttribute('disabled', true);


            console.log("Rezervacija završena! ✅");
        }
        btnDiv2.appendChild(btn2);
    }
}

//useful:
/* btnToActivate.removeAttribute("disabled");
btn3.setAttribute('disabled', true); */