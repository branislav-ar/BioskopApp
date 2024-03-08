import { Bioskop } from "./bioskop.js";

fetch("https://localhost:5001/Bioskop/PreuzmiBioskope")
.then( p => {
    p.json().then(data => {
        data.forEach(b => {
            let bioskop = new Bioskop(b);
            bioskop.crtajBioskop(document.body);
        });
    }); 
});