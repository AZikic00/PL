using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using Models;
using System.Collections.Generic;


namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IgracController : ControllerBase
    {
        public Context Context { get; set; }

        public IgracController(Context context)
        {
            Context = context;
        }

        //---------------------------------------------------------------------------------------------------------

        //              POST METODE

        [Route("Unos_igraca/{Ime}/{Prezime}/{GodinaRodjenja}/{Nacionalnost}/{Golovi}/{Asistencije}/{Naziv_kluba}/{Sezona}")]
        [HttpPost]
        public async Task<ActionResult> Dodaj_igraca(string Ime, string Prezime, int GodinaRodjenja, string Nacionalnost,int Golovi, int Asistencije,string Naziv_kluba,string sezona)
        {
            if (Ime == "") return BadRequest("Morate uneti ime igraca");
            if (Ime.Length > 20) return BadRequest("Pogresna duzina!");

            if (Prezime == "") return BadRequest("Morate uneti ime igraca");
            if (Prezime.Length > 20) return BadRequest("Pogresna duzina!");

            if (Nacionalnost == "") return BadRequest("Morate uneti nacionalnost");

            if (Naziv_kluba == "") return BadRequest("Morate uneti ime Kluba");

            Igrac igrac = new Igrac();

            igrac.Ime = Ime;
            igrac.Prezime = Prezime;
            igrac.GodinaRodjenja = GodinaRodjenja;
            igrac.Nacionalnost = Nacionalnost;
            igrac.Golovi = Golovi;
            igrac.Asistencije = Asistencije;

            var klub = Context.Klubovi.Where(p => p.Naziv.CompareTo(Naziv_kluba) == 0 && p.sezona.Godina.CompareTo(sezona)==0).FirstOrDefault();
            

            if (klub == null)
            {
                return BadRequest($"Uneti klub {Naziv_kluba} ne postoji!");
            }

            igrac.Klub = klub;

            try
            {
                Context.Igraci.Add(igrac);
                await Context.SaveChangesAsync();
                return Ok($"Igrac {Ime} {Prezime} je dodat u bazu!");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------------

        //              DELETE METODE

        [Route("Brisanje_igraca/{Ime}/{Prezime}/{Naziv_kluba}/{Sezona}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi_igraca(string Ime, string Prezime, string Naziv_kluba, string Sezona)
        {
            if (Ime == "") return BadRequest("Morate uneti ime igraca");
            if (Ime.Length > 20) return BadRequest("Pogresna duzina!");

            if (Prezime == "") return BadRequest("Morate uneti prezime igraca");
            if (Prezime.Length > 20) return BadRequest("Pogresna duzina!");

            var klub = Context.Klubovi.Where(p => p.Naziv.CompareTo(Naziv_kluba) == 0 && p.sezona.Godina.CompareTo(Sezona)==0).FirstOrDefault();
            

            if (klub == null)
            {
                return BadRequest($"Uneti klub {Naziv_kluba} ne postoji!");
            }

            try
            {
                var Igrac = Context.Igraci.Where(p => p.Ime.CompareTo(Ime) == 0 && p.Prezime.CompareTo(Prezime) == 0 && p.Klub == klub).FirstOrDefault();
                if (Igrac != null)
                {
                    string Name = Igrac.Ime;
                    string SurName = Igrac.Prezime;

                    Context.Igraci.Remove(Igrac);
                    await Context.SaveChangesAsync();
                    return Ok($"Igrac {Name} {SurName} je uspesno izbrisan!");
                }
                else
                {
                    return Ok("Takav igrac ne postoji!");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------------

        //              GET METODE


        [Route("Svi_igraci_klub/{Klub}/{sezona}")]
        [HttpGet]
        public async Task<List<Igrac>> Svi_igraci_klub(string Klub,string sezona)
        {

            var Igraci = Context.Igraci
            .Include(p=> p.Klub)
            .Include(p=> p.Klub.sezona)
            .Where(p => p.Klub.Naziv.CompareTo(Klub) == 0)
            .Where(p => p.Klub.sezona.Godina.CompareTo(sezona) == 0);

            return await Igraci.ToListAsync();
        }

        [Route("Svi_igraci/{sezona}")]
        [HttpGet]
        public async Task<List<Igrac>> Svi_igraci(string sezona)
        {

            var Igraci = Context.Igraci
            .Include(p=> p.Klub)
            .Include(p=> p.Klub.sezona)
            .Where(p => p.Klub.sezona.Godina.CompareTo(sezona) == 0);

            return await Igraci.ToListAsync();
        }

         //---------------------------------------------------------------------------------------------------------

        //              PUT METODE


        [Route("Promeni_golove/{Ime}/{Prezime}/{Golovi}/{Naziv_kluba}/{Sezona}")]
        [HttpPut]
        public async Task<ActionResult> PromeniGolove(string Ime, string Prezime, int Golovi, string Naziv_kluba, string Sezona)
        {
            if (Ime == "") return BadRequest("Morate uneti ime igraca");
            if (Ime.Length > 20) return BadRequest("Pogresna duzina!");

            if (Prezime == "") return BadRequest("Morate uneti prezime igraca");
            if (Prezime.Length > 20) return BadRequest("Pogresna duzina!");

            if (Golovi < 0) return BadRequest("Pogresna vrednost za promenu golova!");


            var klub = Context.Klubovi.Where(p => p.Naziv.CompareTo(Naziv_kluba) == 0 && p.sezona.Godina.CompareTo(Sezona)==0).FirstOrDefault();
            

            if (klub == null)
            {
                return BadRequest($"Uneti klub {Naziv_kluba} ne postoji!");
            }

            try
            {
                var Igrac = Context.Igraci.Where(p => p.Ime.CompareTo(Ime) == 0 && p.Prezime.CompareTo(Prezime) == 0 && p.Klub == klub).FirstOrDefault();

                Igrac.Golovi = Igrac.Golovi + Golovi;

                Context.Igraci.Update(Igrac);
                await Context.SaveChangesAsync();
                return Ok($"Izmenjeni podaci o igracu {Igrac.Ime} {Igrac.Prezime}, igrac je dao {Igrac.Golovi} golova!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("Promeni_asistencije/{Ime}/{Prezime}/{Asistencije}/{Naziv_kluba}/{Sezona}")]
        [HttpPut]
        public async Task<ActionResult> PromeniAsistencije(string Ime, string Prezime, int Asistencije, string Naziv_kluba, string Sezona)
        {
            if (Ime == "") return BadRequest("Morate uneti ime igraca");
            if (Ime.Length > 20) return BadRequest("Pogresna duzina!");

            if (Prezime == "") return BadRequest("Morate uneti prezime igraca");
            if (Prezime.Length > 20) return BadRequest("Pogresna duzina!");

            if (Asistencije < 0) return BadRequest("Pogresna vrednost za promenu golova!");


            var klub = Context.Klubovi.Where(p => p.Naziv.CompareTo(Naziv_kluba) == 0 && p.sezona.Godina.CompareTo(Sezona)==0).FirstOrDefault();
            

            if (klub == null)
            {
                return BadRequest($"Uneti klub {Naziv_kluba} ne postoji!");
            }

            try
            {
                var Igrac = Context.Igraci.Where(p => p.Ime.CompareTo(Ime) == 0 && p.Prezime.CompareTo(Prezime) == 0 && p.Klub == klub).FirstOrDefault();

                Igrac.Asistencije = Igrac.Asistencije + Asistencije;

                Context.Igraci.Update(Igrac);
                await Context.SaveChangesAsync();
                return Ok($"Izmenjeni podaci o igracu {Igrac.Ime} {Igrac.Prezime}, igrac je asistirao {Igrac.Asistencije} puta!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
