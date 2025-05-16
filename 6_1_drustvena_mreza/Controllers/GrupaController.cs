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

        // Get api/group/{id}
        [HttpGet("{id}")]
        public ActionResult<Grupa> GetById(int id)
        {
            if(!GrupaRepo.grupaRepo.ContainsKey(id))
            {
                return NotFound();
            }

            return Ok(GrupaRepo.grupaRepo[id]);
        }

        // POST api/groups
        [HttpPost]
        public ActionResult<Grupa> Create([FromBody] Grupa newGrupa)
        {
            if (string.IsNullOrWhiteSpace(newGrupa.ime))
            {
                return BadRequest();
            }

            newGrupa.id = SracunajNoviId(GrupaRepo.grupaRepo.Keys.ToList());
            GrupaRepo.grupaRepo[newGrupa.id] = newGrupa;
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
    }
}
