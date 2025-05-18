using _6_1_drustvena_mreza.DOMEN;
using Microsoft.AspNetCore.Http.HttpResults;

namespace _6_1_drustvena_mreza.REPO
{
    public class ClanstvoRepo
    {

        private const string putanjaClanstva = "DATA/clanstva.csv";
        
        public static Dictionary<int, Korisnik> korisnikRepo { get; set; }    

        public static Dictionary<int, Clanstvo> clanstvoRepo { get; set; }

        public ClanstvoRepo()
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
                clanstvoRepo = new Dictionary<int, Clanstvo>();
                string[] sadrzajClanstva = File.ReadAllLines(putanjaClanstva);
                foreach (string linija in sadrzajClanstva)
                {
                    string[] delovi = linija.Split(",");
                    int kljuc = int.Parse(delovi[0]);
                    Clanstvo c = new Clanstvo(int.Parse(delovi[0]), int.Parse(delovi[1]));

                    clanstvoRepo[kljuc] = c;
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void ObrisiKorisnikaIzFile(int userId, int groupId)
        {
            try
            {
                
                string[] allLines = File.ReadAllLines(putanjaClanstva);

                
                List<string> newLines = new List<string>();
                foreach (string line in allLines)
                {
                    string[] parts = line.Split(',');
                    int fileUserId = int.Parse(parts[0]);
                    int fileGroupId = int.Parse(parts[1]);

                    if (!(fileUserId == userId && fileGroupId == groupId))
                    {
                        newLines.Add(line);
                    }
                }

               
                File.WriteAllLines(putanjaClanstva, newLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating memberships file: " + ex.Message);
            }
        }

        public void RemoveUserFromGroup(int userId, int groupId)
        {
            Korisnik user = KorisnikRepo.korisnikRepo[userId];            
            Grupa groupToRemove = null;

            foreach (Grupa group in user.GrupeKorisnika)
            {
                if (group.Id == groupId)
                {
                    groupToRemove = group;
                    break;
                }
            }
                        
            if (groupToRemove != null)
            {
                user.GrupeKorisnika.Remove(groupToRemove);
                Console.WriteLine($"Removed user {userId} from group {groupId}");
                               
                ObrisiKorisnikaIzFile(userId, groupId);
                
            }
            else
            {
                Console.WriteLine("Group not found for this user!");
                
            }
        }

        public bool AddMembership(Clanstvo clanstvo)
        {
            try
            {
                int newId = clanstvoRepo.Keys.DefaultIfEmpty(0).Max() + 1;
                                
                clanstvoRepo[newId] = clanstvo;
                
                File.AppendAllText(putanjaClanstva, $"{clanstvo.KorisnikId},{clanstvo.GrupaId}{Environment.NewLine}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving membership: {ex.Message}");
                return false;
            }
        }

    }
}
