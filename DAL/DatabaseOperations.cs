using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DatabaseOperations
    {
        public static List<Artiest> ophalenArtiesten()
        {
            using (EventEntities entities = new EventEntities() )
            {
                var query = entities.Artiest;
                return query.ToList();
            }
        }

        public static int aanpassenArtiest(Artiest artiest)
        {
            using (EventEntities entities = new EventEntities())
            {
                entities.Entry(artiest).State = System.Data.Entity.EntityState.Modified;
                return entities.SaveChanges();
            }
        }

        public static int toevoegenArtiest(Artiest artiest)
        {
            using (EventEntities entities = new EventEntities())
            {
                entities.Artiest.Add(artiest);
                return entities.SaveChanges();
            }
        }

        public static Event OphalenEvent(int eventId)
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Event
                    .Where(x => x.EventID == eventId);
                return query.SingleOrDefault();
            }
        }

        public static List<Klant> OphalenKlanten()
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Klant;
                return query.ToList();
            }
        }

        public static Klant OphalenKlant(int? klantId)
        {

            using (EventEntities entities = new EventEntities()) 
            {
                var query = entities.Klant
                    .Where(x => x.KlantID == klantId);
                return query.SingleOrDefault();
                  
            }
        }
    }
}
