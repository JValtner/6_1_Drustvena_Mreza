using System;
using _6_1_drustvena_mreza.DOMEN;
using _6_1_drustvena_mreza.REPO;
using Microsoft.AspNetCore.Mvc;

namespace _6_1_Drustvena_Mreza.Controllers
{
    [Route("api/korisnik/{korisnikId}/grupa")]
    [ApiController]
    public class KorisnikGrupeController : ControllerBase
    {
        private KorisnikRepo korisnikRepo = new KorisnikRepo();
        private GrupaRepo grupakRepo = new GrupaRepo();


        [HttpGet("{grupaId}")]
        public ActionResult<Korisnik> NadjiKorisnikaGrupeId(int korisnikId, int grupaId)
        {
            Korisnik korisnik = korisnikRepo.NadjiKorisnika(korisnikId, grupaId);
            if (korisnik ==null)
            {
                return NotFound("Takav korisnik ne postoji");
            }
            return Ok(korisnik);
        }
    }
}


