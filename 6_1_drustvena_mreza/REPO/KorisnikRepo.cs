using System.Collections.Generic;
using _6_1_drustvena_mreza.DOMEN;

namespace _6_1_drustvena_mreza.REPO
{
    public class KorisnikRepo
    {
        private const string putanjaKorisnik = "../../../DATA/korisnici.csv";
        private const string putanjaClanstva = "../../../DATA/clanstva.csv";
        public static Dictionary<int, Korisnik> korisnikRepo { get; set; }

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
                korisnikRepo = new Dictionary<int, Korisnik>();
                string[] sadrzaj = File.ReadAllLines(putanjaKorisnik);
                string[] veze = File.ReadAllLines(putanjaClanstva);
                foreach (string linija in sadrzaj)
                {
                    string[] delovi = linija.Split(",");
                    int kljuc = int.Parse(delovi[0]);
                    List<Grupa> grupeKorisnika = new List<Grupa>();
                    Korisnik k = new Korisnik(int.Parse(delovi[0]), delovi[1], delovi[2], delovi[3], DateTime.ParseExact(delovi[4], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), grupeKorisnika);

                    foreach (string veza in veze)
                    {
                        string[] deloviVeze = veza.Split(",");
                        int idKorisnik = int.Parse(deloviVeze[0]);
                        int idGrupe = int.Parse(deloviVeze[1]);
                        if (k.id.Equals(idKorisnik))
                        {
                            Grupa g = null;
                            foreach (Grupa grupa in GrupaRepo.grupaRepo.Values)
                            {
                                if (grupa.id == idGrupe)
                                {
                                    g = grupa;
                                }
                            }
                            k.grupeKorisnika.Add(g);
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
                    sadrzaj.Add($"{k.id},{k.korisnickoIme},{k.ime},{k.prezime},{k.datumRodjenja.ToString("yyyy-MM-dd")}");
                }
                File.WriteAllLines(putanjaKorisnik, sadrzaj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
