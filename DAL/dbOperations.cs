using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class dbOperations
    {
        public static Event eventOphalen(int eventId)
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Event
                    .Where(x => x.EventID == eventId);

                return query.SingleOrDefault();
            }
        }
        public static List<Event> eventen()
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Event;
                return query.ToList();
            }
        }

        public static List<Notitie> notities()
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Notitie;
                return query.ToList();
            }
        }

        public static string getEventType(int Id)
        {
            using (EventEntities entities = new EventEntities())
            {
                try
                {
                    var query = entities.Eventtype
                        .Where(x => x.EventtypeID == Id)
                        .Select(x => x.Naam);
                    return query.SingleOrDefault().ToString();
                }catch
                {
                    return "";
                }
            }
        }

        public static List<ToDo> todos()
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.ToDo;
                return query.ToList();
            }
        }

        public static int getAllTodosCount()
        {
            using (EventEntities entities = new EventEntities())
            {
                try
                {
                    var query = entities.ToDo;
                    return query.Count();
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static int getTodosCompletedCount()
        {
            using (EventEntities entities = new EventEntities())
            {
                try
                {
                    var query = entities.ToDo
                        .Where(x => x.Afgewerkt == true);
                    return query.Count();
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static Locatie getLocatie(int id)
        {
            using (EventEntities entities = new EventEntities())
            {
                var q = entities.Locatie
                    .Where(x => x.LocatieID == id);
                return q.SingleOrDefault();
            }
        }

        public static Klant GetKlant(int id)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    var q = entities.Klant
                        .Where(x => x.KlantID == id);
                    return q.SingleOrDefault();
                }
            }catch(Exception ex)
            {
                return new Klant();
            }
        }

        public static List<Artiest> getEventArtiesten(int id)
        {
                using (EventEntities e = new EventEntities())
                {
                    var q = e.Artiest
                        .Where(x => x.EventID == id);
                    return q.ToList();
                }
        }


        /*
        // EventTitle
        string EventName = "Rode neuzendag";

        // Notities
        int AantalNotities = 15;
        List<nota> notities;

        // todo
        string Event.todo.completed = "WindowClose"; //Checkbold
        string Event.todo.completeColor = "Black"; //green
        int Event.aantalTodos = 15;

        // Locatie
        Event.Locatie.adres, FallbackValue='Feestzaal Den Eyck'
        string Event.Locatie.plaats = "Kasterlee";
        string Locatie.chLocatie = "Visible";

        // Klant
        string Klant.Bedrijf = "WOKU BVBA";
        string Klant.Naam = FallbackValue="Jonas Vermeiren";
        string Klant.chKlant = "Visible";

        //Artiesten
        string completeColor = 'black';
        bool allTodoCompleted = false;
        string chArtists = 'Visible';

        int Event.AantalArtiesten = 0;
        string Event.chArtiesten = "Visible";

        */
    }
}
