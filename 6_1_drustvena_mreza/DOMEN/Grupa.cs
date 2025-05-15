namespace _6_1_drustvena_mreza.DOMEN
{
    public class Grupa
            {
        public int id;
        public string ime;
        public DateTime datumOsnivanja;

        public Grupa(int id, string ime, DateTime datumOsnivanja)
        {
            this.id = id;
            this.ime = ime;
            this.datumOsnivanja = datumOsnivanja;
        }
    }
}
