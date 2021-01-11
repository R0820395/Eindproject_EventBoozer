using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Event : DataValidatieBasis    {
        public override string this[string propertynaam]
        {
            get
            {
               switch (propertynaam)
                {
                    case "Eventnaam":
                        if (string.IsNullOrWhiteSpace(Eventnaam)) return "Eventnaam moet ingevuld zijn!";
                        if (Eventnaam.Length < 3) return "Eventnaam moet minimaal 3 karakters bevatten.";
                        break;
                        // something is a miss here
                    case "Datum":
                        if (string.IsNullOrWhiteSpace(Datum.ToString())) return "Een datum is verplicht";
                        if (Datum.Year == 1) return "De ingegeven datum is niet geldig.";
                        if (!DateTime.TryParse(Datum.ToString(), out DateTime res))
                        {
                            return "De ingegeven datum is niet geldig.";
                        }
                        break;

                    case "Startuur":
                        if (string.IsNullOrWhiteSpace(Startuur)) return "Een startuur is verplicht.";
                        
                        if (!IsValidTime(Startuur))
                        {
                            return "Het opgegeven startuur is niet geldig.";
                        }
                        break;

                    case "Einduur":
                        if (!string.IsNullOrWhiteSpace(Einduur))
                        
                        if (!IsValidTime(Einduur))
                        {
                            return "Het opgegeven startuur is niet geldig.";
                        }

                        break;
                    case "Naam":
                        if (string.IsNullOrWhiteSpace(Eventtype.Naam)) return "Eventtype moet ingevuld zijn!";

                        string inputt = Eventtype.Naam.ToLower();
                        List<Eventtype> typess = DatabaseOperations.EventTypesOphalen();

                        foreach (Eventtype item in typess)
                        {
                            if (item.Naam.ToLower() == inputt)
                            {
                                return "";
                            }
                        }
                        return "Het opgegeven event type bestaat niet!";
                    default: return "";
                }

                return "";
            }
        }
    }
}

