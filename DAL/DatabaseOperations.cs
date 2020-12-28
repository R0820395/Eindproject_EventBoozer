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
        public static List<Artiest> ophalenArtiesten(int eventID)
        {
            using (EventEntities entities = new EventEntities() )
            {
                var query = entities.Artiest
                    .Where(x => x.EventID == eventID);
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

        public static int verwijderenArtiest(Artiest artiest)
        {
            using (EventEntities entities = new EventEntities())
            {
                entities.Entry(artiest).State = System.Data.Entity.EntityState.Deleted;
                return entities.SaveChanges();
            }
        }

        public static Event OphalenEvent(int eventId)
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Event
                    .Include(x => x.Notities)
                    .Include(x => x.Klant)
                    .Include(x => x.Eventtype)
                    .Include(x => x.ToDos)
                    .Include(x => x.Locatie)
        
                    .Where(x => x.EventID == eventId);
                return query.SingleOrDefault();
            }
        }
        // author: Dieter Daems
        public static List<Event> OphalenAllEvents()
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Event
                    .Include(x => x.Notities)
                    .Include(x => x.Klant)
                    .Include(x => x.Eventtype)
                    .Include(x => x.ToDos)
                    .Include(x => x.Locatie)
                    .Where(x => x.EventID == x.EventID);
                return query.ToList();
            }
        }

        public static bool verwijderEvent(Event ev)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    // entities.Event.Remove(ev);
                    entities.Entry(ev).State = EntityState.Deleted;
                    entities.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static bool ToevoegenEvent(Event ev)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    entities.Event.Add(ev);
                    entities.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<Eventtype> EventTypesOphalen()
        {

            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Eventtype;
                return query.ToList();
            }

        }
        // einde: Dieter Daems

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
            catch
            {
                return 0;
            }

        }

        public static int getAllTodosCount(int eventID)
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.ToDo
                    .Where(x => x.EventID == eventID);
                return query.Count();
            }
        }

        public static int getTodosCompletedCount(int eventID)
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.ToDo
                    .Where(x => x.Afgewerkt == true)
                    .Where(x => x.EventID == eventID);
                return query.Count();
            }
        }




    }
}
