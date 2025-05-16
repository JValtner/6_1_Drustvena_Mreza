using _6_1_drustvena_mreza.DOMEN;
using _6_1_drustvena_mreza.REPO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace _6_1_drustvena_mreza.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GrupaController : ControllerBase
    {
        private GrupaRepo gruparepository = new GrupaRepo();

        // Get api/groups
        [HttpGet]
        public ActionResult<List<Grupa>> GetAll()
        {
            List<Grupa> grupe = GrupaRepo.grupaRepo.Values.ToList();
            return Ok(grupe);
        }

        // POST api/groups
        [HttpPost]
        public ActionResult<Grupa> Create([FromBody] Grupa newGrupa)
        {
            if (string.IsNullOrWhiteSpace(newGrupa.Ime))
            {
                return BadRequest();
            }

            if (newGrupa.Id != 0 && GrupaRepo.grupaRepo.ContainsKey(newGrupa.Id))
            {
                return BadRequest("ID already exists");
            }

            if (newGrupa.Id == 0)
            {
                newGrupa.Id = SracunajNoviId(GrupaRepo.grupaRepo.Keys.ToList());
            }

            GrupaRepo.grupaRepo[newGrupa.Id] = newGrupa;
            gruparepository.Sacuvaj();

            return Ok(newGrupa);
        }

        private int SracunajNoviId(List<int> identifikatori)
        {
            int maxId = 0;
            foreach (int id in identifikatori)
            {
                if (id > maxId)
                {
                    maxId = id;
                }
            }

            return maxId + 1;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!GrupaRepo.grupaRepo.ContainsKey(id))
            {
                return NotFound();
            }

            GrupaRepo.grupaRepo.Remove(id);
            gruparepository.Sacuvaj();

            return NoContent();
        }
    }
}
