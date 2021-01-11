using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Eventtype : DataValidatieBasis
    {
        public override string this[string propertynaam]
        {
            get
            {
                switch (propertynaam)
                {
                    case "Naam":
                        if (string.IsNullOrWhiteSpace(Naam)) return "Eventtype moet ingevuld zijn!";

                        string inputt = Naam.ToLower();
                        List<Eventtype> typess = DatabaseOperations.EventTypesOphalen();

                        foreach (Eventtype item in typess)
                        {
                            if (item.Naam.ToLower() == inputt)
                            {
                                return "";
                            }
                        }
                        return "Gelieven te kiezen tussen: Fuif, Vat, Trouwfeest";
                    default: return "";
                }
            }
        }
    }
}
