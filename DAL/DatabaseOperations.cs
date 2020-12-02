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
    }
}
