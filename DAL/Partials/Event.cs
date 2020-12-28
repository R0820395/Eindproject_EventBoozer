using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Partials
{
    public partial class Event : IDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                string error = null;
                    
                if(name == "Eventnaam")
                {
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        error = "Je moet een naam ingeven voor het Event.";
                    }
                }

                return error;
            }
        }


        public string Error
        {
            get
            {
                return null;
            }
        }
    }
}
