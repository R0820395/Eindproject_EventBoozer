using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public partial class Artiest
    {
        public override string ToString()
        {
            return Naam.ToString();

            // $"{this.naam} \n {this.Telefoon} \n {this.Email}"
        }
    }
}
