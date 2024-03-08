import { Film } from "./film.js";

export class formaBioskopa {

    constructor (bioskopRef, instancaBioskopaRef) {
        
        this.bioskopRef = bioskopRef;
        this.id = bioskopRef.id;
        this.kontejnerFormeBioskopa = null;
        
        this.stavke = [];
        instancaBioskopaRef.stavke.forEach(filmEL => {
            const film = new Film(filmEL.id, filmEL.naziv, filmEL.cenaKarte)
            this.stavke.push(film);
        });
    }

    dodajStavku() {

        let naz = document.querySelector(".nazivFilmaInput" + this.id);
        var cena = document.querySelector(".cenaFilmaInput" + this.id);
        var l  = document.querySelector(".stavkeSelect" + this.id);
        let index = Date.now();

        if (cena.value !== "") {
            fetch("https://localhost:5001/Bioskop/UpisiFilm/" + this.id,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(
                    {
                        "naziv": naz.value.toString(),
                        "cenaKarte": parseInt(cena.value)
                    }
                )
            }).then(p => {
                if (p.ok) {
                    let stavka = document.createElement("option");
                    stavka.classList.add("stavkaFormaBioskopa");

                    stavka.value = cena.value;
                    stavka.name = naz.value;
                    stavka.innerHTML = naz.value;
                    l.add(stavka);

                    p.json().then ( p => {
                        let s = new Film(index, naz.value, cena.value);
                        this.stavke.push(s);
                        this.bioskopRef.dodajFilmSali();
                    });
                    
                    console.log("Uspešno dodavanje! ✅")
                    alert(`🎥: ${naz.value}\n🏷️: ${cena.value}RSD\n\n\n ✅ USPEŠNO DODAT ✅`);
                    
                }
                else if (p.status == 406) {
                    console.log("Greška u unetim podacima! ⚠️")
                    alert("Greška prilikom upisa. Proverite podatke. ⚠️");
                }
                else {
                    console.log("⚠️")
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
            });
        }
        else {
            console.log("⚠️")
            alert("Greška prilikom upisa. Proverite podatke. ⚠️");
        }
    }    

    obrisiStavku() {

        let lista = document.querySelector(".stavkeSelect" + this.id);
        let ind = lista.options.selectedIndex;
        let stavka = this.stavke[ind];

        fetch("https://localhost:5001/Bioskop/ObrisiFilm/" + stavka.id,
            {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ })

            }).then(p => {
                if (p.ok) {

                    lista.options[ind] = null;
                    delete lista.options[ind];
                    
                    this.bioskopRef.ukloniFilmSali(ind);

                    console.log("Uspešno brisanje! ✅")
                    alert(`🎥: ${stavka.naziv}\n\n\n ❌ USPEŠNO OBRISAN! ❌`);
                }
                else
                {
                    alert("Doslo je do greske prilikom brisanja.");
                }
            });
    }

    izmeniStavku(index) {
        let naz = document.querySelector(".nazivFilmaInput" + this.id);
        let cena = document.querySelector(".cenaFilmaInput" + this.id);

        let lista = document.querySelector(".stavkeSelect" + this.id);
        let stavka = lista.options[index]
        let s = this.stavke[index];

        if (cena.value !== "") {
            fetch("https://localhost:5001/Bioskop/IzmeniFilm/" + s.id, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify ({
                    "naziv": naz.value.toString(),
                    "cenaKarte": parseInt(cena.value)
                })
            }).then(p => {
                if (p.ok){
                    s.naziv = lista.options[index].name = naz.value;
                    s.cena = lista.options[index].value = cena.value;
                    lista.options[index].innerHTML = naz.value;

                    this.bioskopRef.azurirajFilmSali(index, stavka);

                    console.log("Uspešno ažiriranje! ✅")
                    alert(`NOVE VREDNOSTI:\n\n🎥: ${s.naziv}\n🏷️: ${s.cena}RSD\n\n\n  USPEŠNO AŽURIRANJE! 🆕`);
                }
                else if (p.status == 406) {
                    console.log("Greška u unetim podacima! ⚠️")
                    alert("Greška prilikom upisa. Proverite podatke. ⚠️");
                }
                else {
                    console.log("⚠️")
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
            });
        }
        else {
            console.log("⚠️")
            alert("Greška prilikom upisa. Proverite podatke. ⚠️");
        }        
    }
    
    crtajFormuBioskopa(host) {
        if(!host) {
            throw new Error("Host element za 'formaBioskopa1' ne postoji!");
        }

        this.kontejnerFormeBioskopa = document.createElement("div");
        let id = "kontejnerFormeBioskopa" + this.id;
        this.kontejnerFormeBioskopa.classList.add(id);
        this.kontejnerFormeBioskopa.classList.add("kontejnerFormeBioskopaDIV");
        host.appendChild(this.kontejnerFormeBioskopa);

        let naslovForme_DIV = document.createElement("div");
        naslovForme_DIV.classList.add("naslovForme_DIV");

        let naslovForme = document.createElement("label");
        naslovForme.innerHTML = "MENI FILMOVA";
        naslovForme.classList.add("naslovForme");
        naslovForme_DIV.appendChild(naslovForme);
        this.kontejnerFormeBioskopa.appendChild(naslovForme_DIV);

        this.crtajFormu(this.kontejnerFormeBioskopa);
    }

    crtajFormu(host) {
        if(!host) {
            throw new Error("Host element za 'formaBioskopa' ne postoji!");
        }

        //labele, elementi za unos i dugmići FORM dela
        //veliki DIV za sve to
        const forma = document.createElement("form");
        forma.classList.add("formaLabele_Unos_Dugme");
        host.appendChild(forma);
        
        //SELECT DEO
        //select deo SELECT dela
        let sel_DIV = document.createElement("div");
        sel_DIV.classList.add("sel_DIV");

        const sel = document.createElement("select");
        sel.classList.add("stavkeSelect" + this.id);
        sel.setAttribute('multiple', true);
        sel.size = 8;
        sel_DIV.appendChild(sel);
        forma.appendChild(sel_DIV);

        let stavka;
        this.stavke.forEach( filmEL => {
            stavka = document.createElement("option");
            stavka.classList.add("stavkaFormaBioskopa");

            stavka.value = filmEL.cenaKarte;
            stavka.name = filmEL.naziv;
            stavka.innerHTML = filmEL.naziv;
            sel.appendChild(stavka);
        });

        //događaj koji upisuje odgovarajući tekst u polja
        //pri odabiru stavke
        sel.onchange=(event) => {
            let i = sel.selectedIndex;
            if(i >= 0) {
                let btnToActivate = document.querySelector(".dugme_activate1");
                btnToActivate.removeAttribute("disabled");

                btnToActivate = document.querySelector(".dugme_activate2");
                btnToActivate.removeAttribute("disabled");

                btnToActivate = document.querySelector(".dugme_activate3");
                btnToActivate.removeAttribute("disabled");

                let naz = document.querySelector(".nazivFilmaInput" + this.id);
                naz.value = sel.options[i].name;
                
                let cena = document.querySelector(".cenaFilmaInput" + this.id);
                cena.value = sel.options[i].value;

                console.log(`Selektovana opcija: 🎥: ${naz.value}, 🏷️: ${cena.value}RSD`);
            }
        }

        //NAZIV
        let lab_DIV = document.createElement("div");
        lab_DIV.classList.add("lab_DIV");
        let input_DIV = document.createElement("div");
        input_DIV.classList.add("input_DIV");
        //labela
        let lab = document.createElement("label");
        lab.classList.add("formaLabela");
        lab.name = "naziv";
        lab.innerHTML = "NAZIV: ";

        lab_DIV.appendChild(lab);
        
        //input
        let input = document.createElement("input");
        input.type = "text";
        input.name = "naziv";
        let id = "nazivFilmaInput" + this.id;
        input.classList.add("nazivFilmaInput" + this.id);
        input.classList.add("formaInput");

        input_DIV.appendChild(input);

        forma.appendChild(lab_DIV);
        forma.appendChild(input_DIV);

        //aktivacija dugmića na unos
        input.onclick=(event) => {
                let btnToActivate = document.querySelector(".dugme_activate1");
                btnToActivate.removeAttribute("disabled");

                btnToActivate = document.querySelector(".dugme_activate2");
                btnToActivate.removeAttribute("disabled");

                btnToActivate = document.querySelector(".dugme_activate3");
                btnToActivate.removeAttribute("disabled");
        }

        //CENA KARTE
        //labela
        lab = document.createElement("labela");
        lab.classList.add("formaLabela", "CENA_KARTE");
        lab.name = "cenaKarte";
        lab.innerHTML = "CENA: ";

        lab_DIV.appendChild(lab)
        
        //input
        input = document.createElement("input");
        input.name = "cenaKarte";
        id = "cenaFilmaInput" + this.id;
        input.classList.add(id);
        input.classList.add("formaInput");

        input_DIV.appendChild(input);

        let lab_inputDIV = document.createElement("div");
        lab_inputDIV.className = "lab_inputDIV";

        lab_inputDIV.appendChild(lab_DIV);
        lab_inputDIV.appendChild(input_DIV);
        forma.appendChild(lab_inputDIV);

        //DUGMICI
        //Dugme za dodavanje filma
        let btn_DIV = document.createElement("div");
        btn_DIV.className = "btn_DIV";

        const btn1 = document.createElement("button");
        btn1.setAttribute('disabled', true);
        btn1.type = "button";
        btn1.classList.add("dugme", "dugme_activate1");
        btn1.innerHTML = "DODAJ FILM";
        btn1.value = "Dodaj stavku";

        btn1.onclick=(event) => {
            this.dodajStavku();
        }

        btn_DIV.appendChild(btn1);
        forma.appendChild(btn_DIV);

        //Dugme za brisanje filma
        btn_DIV = document.createElement("div");
        btn_DIV.className = "btn_DIV";

        const btn2 = document.createElement("button");
        btn2.setAttribute('disabled', true);
        btn2.type = "button";
        btn2.classList.add("dugme", "dugme_activate2");
        btn2.innerHTML = "OBRIŠI FILM";
        btn2.value = "Obrisi stavku";

        btn2.onclick=(event) => {
            this.obrisiStavku();
        }

        btn_DIV.appendChild(btn2);
        forma.appendChild(btn_DIV);

        //Dugme za izmenu filma
        btn_DIV = document.createElement("div");
        btn_DIV.className = "btn_DIV";

        const btn3 = document.createElement("button");
        btn3.setAttribute('disabled', true);
        btn3.type = "button";
        btn3.classList.add("dugme", "dugme_activate3");
        btn3.innerHTML = "IZMENI FILM";
        btn3.value = "Izmeni stavku";

        btn3.onclick=(event) => {
            let lista = document.querySelector(".stavkeSelect" + this.id);
            let i = lista.options.selectedIndex;
            this.izmeniStavku(i);
        }

        btn_DIV.appendChild(btn3);
        forma.appendChild(btn_DIV);
    }

}