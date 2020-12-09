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
                var query = entities.Eventtype
                    .Where(x => x.EventtypeID == Id)
                    .Select(x => x.Naam);
                return query.SingleOrDefault().ToString();
            }
        }

        public static int getAllTodosCount()
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.ToDo;
                return query.Count();
            }
        }

        public static int getTodosCompletedCount()
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.ToDo
                    .Where(x => x.Afgewerkt == true);
                return query.Count();
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
