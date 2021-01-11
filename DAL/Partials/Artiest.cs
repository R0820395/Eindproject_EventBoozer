using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Text.RegularExpressions;

namespace DAL
{
    public partial class Artiest: Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Bankrekeningnr" && Regex.Match(Bankrekeningnr, @"^BE\d{14}$").Success == false)
                {
                    return "Bankrekening moet beginnen met BE en heeft 14 cijfers.";
                }
                
                if (columnName == "Email" && (!Email.Contains("@") || !Email.Contains(".")))
                {
                    return "Gelieve een geldig emailadres in te geven";
                }
                return "";
                
            }
        }

        public override string ToString()
        {
            return Naam.ToString();

            // $"{this.naam} \n {this.Telefoon} \n {this.Email}"
        }
        
       
    }
}
