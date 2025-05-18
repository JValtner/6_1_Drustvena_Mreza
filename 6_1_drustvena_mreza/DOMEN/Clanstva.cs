using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _6_1_drustvena_mreza.DOMEN
{
    public class Clanstvo
    {

        public int KorisnikId { get; set; }        
        public int GrupaId { get; set; }      


        public Clanstvo(int korisnikId, int grupaId)
        {
            this.KorisnikId = korisnikId;
            this.GrupaId= grupaId;           
        }
    }
}
