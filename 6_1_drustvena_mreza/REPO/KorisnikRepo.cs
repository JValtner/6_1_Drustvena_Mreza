using System.Collections.Generic;
using System;
using _6_1_drustvena_mreza.DOMEN;

namespace _6_1_drustvena_mreza.REPO
{
    public class KorisnikRepo
    {
        private const string putanjaKorisnik = "DATA/korisnici.csv";
        private const string putanjaClanstva = "DATA/clanstva.csv";
        private const string putanjaGrupe = "DATA/grupe.csv";
        public static Dictionary<int, Korisnik> korisnikRepo { get; set; }
        public static Dictionary<int, Grupa> grupaRepo { get; set; }       

        public KorisnikRepo()
        {
            if (korisnikRepo == null)
            {
                Procitaj();
            }
        }
        public void Procitaj()
        {
            try
            {
                //Procitaj grupe

                grupaRepo = new Dictionary<int, Grupa>();
                string[] sadrzajGrupe = File.ReadAllLines(putanjaGrupe);
                foreach (string linija in sadrzajGrupe)
                {
                    string[] delovi = linija.Split(",");
                    int kljuc = int.Parse(delovi[0]);
                    Grupa g = new Grupa(int.Parse(delovi[0]), delovi[1], DateTime.ParseExact(delovi[2], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));

                    grupaRepo[kljuc] = g;
                }
                //Procitaj Korisnike
                korisnikRepo = new Dictionary<int, Korisnik>();
                string[] sadrzaj = File.ReadAllLines(putanjaKorisnik);
                string[] veze = File.ReadAllLines(putanjaClanstva);
                foreach (string linija in sadrzaj)
                {
                    string[] delovi = linija.Split(",");
                    int kljuc = int.Parse(delovi[0]);
                    List<Grupa> grupeKorisnika = new List<Grupa>();
                    Korisnik k = new Korisnik(int.Parse(delovi[0]), delovi[1], delovi[2], delovi[3], DateTime.ParseExact(delovi[4], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    foreach (string veza in veze)
                    //Dodeli grupe korisnicima
                    {
                        string[] deloviVeze = veza.Split(",");
                        int idKorisnik = int.Parse(deloviVeze[0]);
                        int idGrupe = int.Parse(deloviVeze[1]);
                        if (k.Id.Equals(idKorisnik))
                        {
                            Grupa g = null;
                            foreach (Grupa grupa in grupaRepo.Values)
                            {
                                if (grupa.Id == idGrupe)
                                {
                                    g = grupa;
                                }
                            }
                            k.GrupeKorisnika.Add(g);
                        }
                    }

                    korisnikRepo[kljuc] = k;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Sacuvaj()
        {
            try
            {
                List<string> sadrzaj = new List<string>();
                foreach (KeyValuePair<int, Korisnik> entryValue in korisnikRepo)
                {
                    Korisnik k = entryValue.Value;
                    sadrzaj.Add($"{k.Id},{k.KorisnickoIme},{k.Ime},{k.Prezime},{k.DatumRodjenja.ToString("yyyy-MM-dd")}");
                }
                File.WriteAllLines(putanjaKorisnik, sadrzaj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Korisnik NadjiKorisnika(int korisnikId, int grupaId)
        {

            foreach (Korisnik korisnik in KorisnikRepo.korisnikRepo.Values)
            {
                if (korisnik.Id == korisnikId)
                {
                    foreach (Grupa grupa in korisnik.GrupeKorisnika)
                    {
                        if (grupa.Id == grupaId)
                        {

                            return korisnik;
                        }
                    }
                }
            }
            return null;
        }
    }
}
