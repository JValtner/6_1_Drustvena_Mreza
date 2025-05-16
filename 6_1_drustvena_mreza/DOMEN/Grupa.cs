namespace _6_1_drustvena_mreza.DOMEN
{
    public class Grupa
            {
        public int id { get; set; }
        public string ime { get; set; }
        public DateTime datumOsnivanja { get; set; }

        public Grupa(int id, string ime, DateTime datumOsnivanja)
        {
            this.id = id;
            this.ime = ime;
            this.datumOsnivanja = datumOsnivanja;
        }
    }
}
