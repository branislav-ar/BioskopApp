using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;
using Microsoft.EntityFrameworkCore;
using Server;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BioskopController : ControllerBase
    {

        public BioskopContext Context { get; set; }

        public BioskopController(BioskopContext context)
        {
            Context = context;
        }

        //PRIMARNI DEO
        //I učiavanje svih filmova
        [Route("PreuzmiBioskope")]
        [HttpGet]
        public async Task<List<Bioskop>> PreuzmiBioskope()
        {
            return await Context.Bioskopi
            .Include(p => p.sale).ThenInclude(q => q.VEZA_Sala_Film)
            .Include(p => p.sale).ThenInclude(q => q.VEZA_Sala_Sediste)
            .Include(x => x.formaBioskopa).ThenInclude(q => q.Stavke).ToListAsync();
        }

        //II dodavanje filma
        [Route("UpisiFilm/{id}")]
        [HttpPost]
        /*
            {
                "Naziv": "The Godfather",
                "cenaKarte": 320
            }
        */
        public async Task<IActionResult> UpisiFilm(int id, [FromBody] Film f)
        {   
            if(f.cenaKarte > 0 && f.naziv.Length-1 > 0)
            {
                var formaB = await Context.FormeBioskopa.FindAsync(id);
                f.FormaBioskopaID = formaB.ID;

                Context.Filmovi.Add(f);
                await Context.SaveChangesAsync();
                return Ok(f.id);
            }
            else {
                return StatusCode(406);
            }
        }

        //III brisanje filma
        [Route("ObrisiFilm/{idFilma}")]
        [HttpDelete]
        /*
            body: none
        */
        public async Task ObrisiFilm(int idFilma) {   

            var film = await Context.Filmovi.FindAsync(idFilma);
            Context.Remove(film);
            await Context.SaveChangesAsync();
        }

        //III ažuriranje filma
        [Route("IzmeniFilm/{idFilma}")]
        [HttpPut]
        /*
            {
                "Naziv": "The Godfather",
                "cenaKarte": 320
            }
        */
        public async Task<IActionResult> IzmeniFilm(int idFilma, [FromBody] Film f) {   
            
            var film = await Context.Filmovi.FindAsync(idFilma);

            if(f.naziv.Length-1 > 0 && f.cenaKarte > 0)
            {
                film.naziv = f.naziv;
                film.cenaKarte = f.cenaKarte;

                Context.Filmovi.Update(film);
                await Context.SaveChangesAsync();
                return Ok();
            }
            else {
                return StatusCode(406);
            }
        }

        //SEKUNDARNI DEO
        [Route("PreuzmiFilmove")]
        [HttpGet]
        public async Task<List<Film>> PreuzmiFilmove()
        {
            return await Context.Filmovi.Include(x => x.Veza).ToListAsync();
        }

        [Route("PreuzmiFilmoveIzSale/{idSale}")]
        [HttpGet]
        public async Task<List<SalaFilm>> PreuzmiFilmoveIzSale(int idSale)
        {
            return await Context.SalaFilmVeza.Where(p => p.salaID == idSale).ToListAsync();
        }

        [Route("PreuzmiSedistaIzSale")]
        [HttpGet]
        public async Task<List<Sediste>> PreuzmiSedistaIzSale()
        {
            return await Context.Sedista
            .Include(p => p.Veza).ThenInclude(q => q.sala)
            .ToListAsync();
        }       

        //UPIS - HttpPost
        [Route("UpisiBioskop")]
        [HttpPost]
        /*
            {
	            "naziv": Bioskop1,
	            "brojsala": 3,
	            "brojmestausalama": 20
	        }
        */
        public async Task UpisiBioskop([FromBody] Bioskop b) {
                Context.Bioskopi.Add(b);
                await Context.SaveChangesAsync();
        }

        [Route("UpisiSalu/{idBioskopa}")]
        [HttpPost]
        /*
            {
                "broj": 1,
                "naziv": "Sala1"
            }
        */
        public async Task UpisiSalu(int idBioskopa, [FromBody] Sala s) {
                var bioskop = await Context.Bioskopi.FindAsync(idBioskopa);
                s.bioskop = bioskop;
                Context.Sale.Add(s);
                await Context.SaveChangesAsync();
        }

        [Route("DodajFilm/{idSale}/{idFilma}")]
        [HttpPost]
        public async Task DodajFilm(int idFilma, int idSale, [FromBody] SalaFilm sf) {
            Context.SalaFilmVeza.Add(sf);
            await Context.SaveChangesAsync();
        }

        [Route("DodajSedisteUSalu/{idSale}/{idSedista}")]
        [HttpPost]
        /*
            {
                "sedisteID": 2,
                "salaID": 1 //Sala 1, bioskop 1
            }
        */
        public async Task DodajSedisteUSalu(int idSale, int idSedista, [FromBody] SalaSediste ss) {
                var sediste = await Context.Sedista.FindAsync(idSedista);
                ss.sediste = sediste;

                var sala = await Context.Sale.FindAsync(idSale);
                ss.sala = sala;

                Context.SalaSedisteVeza.Add(ss);
                await Context.SaveChangesAsync();
        }

        [Route("UpisiFormuBioskopa/{idBioskopa}")]
        [HttpPost]
        /*
            {

            }
        */
        public async Task UpisiFormuBioskopa(int idBioskopa, [FromBody] FormaBioskopa fb) {
            var bioskop = await Context.Bioskopi.FindAsync(idBioskopa);
            fb.bioskop = bioskop;
            Context.FormeBioskopa.Add(fb);
            await Context.SaveChangesAsync();
        }

        [Route("UpisiSediste/{id}")]
        [HttpPost]
        /*
            {
                "Zauzetost": false
            }
        */
        public async Task UpisiSediste(int id, [FromBody] Sediste s) {   
            var formaB = await Context.FormeBioskopa.FindAsync(id);

            s.FormaBioskopaID = formaB.ID;

            Context.Sedista.Add(s);
            await Context.SaveChangesAsync();
        }

        //IZMENI - HttpPut
        [Route("IzmeniBioskop/{idBioskopa}")]
        [HttpPut]
        /*
            {
                "naziv": "BioskopProba",
                "brojsala": 3,
                "brojmestausalama": 20
            }
        */
        public async Task IzmeniBioskop(int idBioskopa, [FromBody] Bioskop b) {
            
            var bioskop = await Context.Bioskopi.FindAsync(idBioskopa);

            bioskop.naziv = b.naziv;
            bioskop.brojSala = b.brojSala;
            bioskop.brojMestaUSalama = b.brojMestaUSalama;

            Context.Bioskopi.Update(bioskop);
            await Context.SaveChangesAsync();
        }
        
        [Route("IzmeniSalu/{idSale}")]
        [HttpPut]
        public async Task IzmeniSalu(int idSale, [FromBody] Sala s) {
                
            var sala = await Context.Sale.FindAsync(idSale);
            //var bioskop = await Context.Bioskopi.FindAsync(idBioskopa);
                
            //sala.bioskop = bioskop;
            sala.broj = s.broj;
            sala.naziv = s.naziv;

            Context.Sale.Update(sala);
            await Context.SaveChangesAsync();
        }

        [Route("ZauzmiSediste/{broj}")]
        [HttpPut]
        /*
            {
            }
        */
        public async Task ZauzmiSediste(int broj) {   
            var sediste = await Context.Sedista.FindAsync(broj);

            sediste.zauzetost = true;

            Context.Sedista.Update(sediste);
            await Context.SaveChangesAsync();
        }

        [Route("OslobodiSediste/{broj}")]
        [HttpPut]
        /*
            {
            }
        */
        public async Task OslobodiSediste(int broj) {   
            var sediste = await Context.Sedista.FindAsync(broj);

            sediste.zauzetost = false;

            Context.Sedista.Update(sediste);
            await Context.SaveChangesAsync();
        }

        //BRISANJE - HttpDelete
        //!
        [Route("ObrisiSalu/{idSale}")]
        [HttpDelete]
        /*
            body: none
        */
        public async Task ObrisiSalu(int idSale) {

            var sala = await Context.Bioskopi.FindAsync(idSale);
            Context.Remove(sala);
            await Context.SaveChangesAsync();
        }       
    }
}
