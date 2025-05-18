using System;
using System.Linq;
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
        private ClanstvoRepo clanstvoRepo = new ClanstvoRepo();


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

        [HttpPost("{groupId}")]
        public ActionResult<List<Clanstvo>> AddUserToGroup(int korisnikId, int groupId)
        {
            if (!KorisnikRepo.korisnikRepo.ContainsKey(korisnikId))
            {
                return NotFound($"User {korisnikId} not found");
            }

            if (!GrupaRepo.grupaRepo.ContainsKey(groupId))
            {
                return NotFound($"Group {groupId} not found");
            }
                

            if (ClanstvoRepo.clanstvoRepo.Values.Any(c => c.KorisnikId == korisnikId && c.GrupaId == groupId))
            {
                return BadRequest($"User {korisnikId} already has that role {groupId} assigned to him");
            }

            Clanstvo noviClan = new Clanstvo(korisnikId, groupId);
            clanstvoRepo.AddMembership(noviClan);
            


            return Ok("test");
        }

        [HttpDelete("{groupId}")]
        public ActionResult RemoveUserFromGroup(int korisnikId, int groupId)
        {
            
            if (!KorisnikRepo.korisnikRepo.ContainsKey(korisnikId))
            {
                return NotFound("User not found!");
            }

            if (!GrupaRepo.grupaRepo.ContainsKey(groupId))
            {
                return NotFound("Group not found!");
            }

            if(!ClanstvoRepo.clanstvoRepo.Values.Any(c => c.KorisnikId == korisnikId && c.GrupaId == groupId))
            {
                return NotFound("Korisnik nema grupu");
            }

            clanstvoRepo.RemoveUserFromGroup(korisnikId, groupId);

            return Ok($"User {korisnikId} was removed from group {groupId}");         
        }
    }
}


