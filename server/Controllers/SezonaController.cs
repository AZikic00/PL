using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Models;


namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SezonaController : ControllerBase
    {
        public Context Context { get; set; }

        public SezonaController(Context context)
        {
            Context = context;
        }


        //---------------------------------------------------------------------------------------------------------

        //              POST METODE

        [Route("Unos_utakmice/{godina}/{domacin}/{golovidomacin}/{gost}/{golovigost}/{sudijaime}/{sudijaprezime}/{kolo}")]
        [HttpPost]
        public async Task<ActionResult> Dodaj_utakmice(string godina, string domacin, int golovidomacin, string gost,int golovigost, string sudijaime ,string sudijaprezime,int kolo)
        {
            if (godina == "") return BadRequest("Morate uneti sezonu!");
            if (godina.Length > 10) return BadRequest("Pogresan format sezone!");
            
            if (domacin == "") return BadRequest("Morate uneti naziv domacina");
            if (domacin.Length > 50) return BadRequest("Pogresna duzina!");

            if (gost == "") return BadRequest("Morate uneti naziv gosta");
            if (gost.Length > 50) return BadRequest("Pogresna duzina!");

            if (kolo<0 && kolo>38) return BadRequest("Kolo ne postoji!");

            if (golovidomacin<0) return BadRequest("Pogresan broj golova domacina!");

            if (golovigost<0) return BadRequest("Pogresan broj golova gosta!");

            if (sudijaime == "") return BadRequest("Morate uneti ime sudije");
            if (sudijaime.Length > 20) return BadRequest("Pogresna duzina!");

            if (sudijaprezime == "") return BadRequest("Morate uneti prezime sudije");
            if (sudijaprezime.Length > 20) return BadRequest("Pogresna duzina!");

            Utakmica utakmica = new Utakmica();

            var sezona = Context.Sezone.Where(p => p.Godina.CompareTo(godina)==0).FirstOrDefault();
            if (sezona == null)
            {
                return BadRequest($"Uneta sezona {godina} ne postoji!");
            }

            var Domacin = Context.Klubovi.Where(p => p.Naziv.CompareTo(domacin)==0 && p.sezona.Godina.CompareTo(godina)==0).FirstOrDefault();
            if (Domacin == null)
            {
                return BadRequest($"Uneti klub {domacin} ne postoji!");
            }

            var Gost = Context.Klubovi.Where(p => p.Naziv.CompareTo(gost)==0 && p.sezona.Godina.CompareTo(godina)==0).FirstOrDefault();
            if (Gost == null)
            {
                return BadRequest($"Uneti klub {gost} ne postoji!");
            }

            var Sudija = Context.Sudije.Where(p => p.Ime.CompareTo(sudijaime)==0 && p.Prezime.CompareTo(sudijaprezime)==0).FirstOrDefault();
            if (Sudija == null)
            {
                return BadRequest($"Uneti sudija {sudijaime} {sudijaprezime} ne postoji!");
            }

            utakmica.Sezona = sezona;
            utakmica.Domacin = Domacin;
            utakmica.golovi_domacin =golovidomacin;
            utakmica.Gost = Gost;
            utakmica.golovi_gost = golovigost;
            utakmica.sudija = Sudija;
            utakmica.Kolo = kolo;


            try
            {
                Context.Utakmice.Add(utakmica);
                await Context.SaveChangesAsync();
                return Ok($"Utakmica {domacin}-{gost} je dodata u bazu!");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------------

        //              GET METODE

        [Route("Pregledaj_sezone")]
        [HttpGet]
        public async Task<List<Sezona>> Vrati_sezone()
        {

            var sezone = Context.Sezone;

            return await sezone.ToListAsync();
        }

        

        [Route("Pogledaj_kolo/{godina}/{BrKola}")]
        [HttpGet]
        public async Task<List<Utakmica>> Vrati_kolo(string godina,int BrKola)
        {
            var Kolo_utakmice=Context.Utakmice
            .Include(p=>p.Domacin)
            .Include(p=>p.Gost)
            .Include(p=>p.sudija)
            .Include(p=>p.Sezona)
            .Where(p=>p.Kolo==BrKola)
            .Where(p => p.Sezona.Godina.CompareTo(godina) == 0);

            return await Kolo_utakmice.ToListAsync();
        }

        [Route("Sve_utakmice/{godina}")]
        [HttpGet]
        public async Task<List<Utakmica>> Sve_utakmice(string godina)
        {

            var Sve_utakmice=Context.Utakmice
            .Include(p=>p.Domacin)
            .Include(p=>p.Gost)
            .Include(p=>p.sudija)
            .Include(p=>p.Sezona)
            .Where(p => p.Sezona.Godina.CompareTo(godina) == 0);

            return await Sve_utakmice.ToListAsync();
        }

        [Route("Sve_utakmice_klub/{godina}/{naziv}")]
        [HttpGet]
        public async Task<List<Utakmica>> Sve_utakmice_klub(string godina,string naziv)
        {

            var Klub_utakmice=Context.Utakmice
            .Include(p=>p.Domacin)
            .Include(p=>p.Gost)
            .Include(p=>p.sudija)
            .Include(p=>p.Sezona)
            .Where(p => p.Sezona.Godina.CompareTo(godina) == 0)
            .Where(p=>(p.Domacin.Naziv.CompareTo(naziv)==0 || p.Gost.Naziv.CompareTo(naziv)==0));

            return await Klub_utakmice.ToListAsync();
        }

    }
}