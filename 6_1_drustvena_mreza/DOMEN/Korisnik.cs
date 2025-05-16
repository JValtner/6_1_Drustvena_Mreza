using System;
namespace _6_1_drustvena_mreza.DOMEN
{
    public class Korisnik
    {
        public int id;
        public string korisnickoIme;
        public string ime;
        public string prezime;
        public DateTime datumRodjenja;
        public List<Grupa> grupeKorisnika = new List<Grupa>();

        public Korisnik (int id, string korisnickoIme, string ime, string prezime, DateTime datumRodjenja, List<Grupa>grupeKorisnika)
        {
            this.id = id;
            this.korisnickoIme = korisnickoIme;
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodjenja;
            this.grupeKorisnika = grupeKorisnika;
        }
    }
}
