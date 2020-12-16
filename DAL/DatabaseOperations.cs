﻿/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*+======================================================================= Artiesten:   Jan ===============================================================================================+*/
/*+======================================================================= Klanten:     Nisse =============================================================================================+*/
/*+======================================================================= Events:      Dieter ============================================================================================+*/
/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public static class DatabaseOperations
    {
        public static List<Artiest> ophalenArtiesten(int eventID)
        {
            using (EventEntities entities = new EventEntities())
            {
                var query = entities.Artiest
                    .Where(x => x.EventID == eventID);
                return query.ToList();
            }
        }

        public static int aanpassenArtiest(Artiest artiest)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    entities.Entry(artiest).State = System.Data.Entity.EntityState.Modified;
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
            
        }

        public static int toevoegenArtiest(Artiest artiest)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    entities.Artiest.Add(artiest);
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
            
        }

        public static int verwijderenArtiest(Artiest artiest)
        {
            try
            {
                using (EventEntities entities = new EventEntities())
                {
                    entities.Entry(artiest).State = System.Data.Entity.EntityState.Deleted;
                    return entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
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
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);

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