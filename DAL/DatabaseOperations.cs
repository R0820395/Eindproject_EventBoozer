using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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

        public static int ToevoegenKlant(Klant klant)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    entities.Klant.Add(klant);
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int AanpassenEvent(Event aangepastevent)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    entities.Entry(aangepastevent).State = EntityState.Modified;
                    return entities.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                FileOperations.FoutLoggen(ex);

                return 0;
            }

        }


    }
}
