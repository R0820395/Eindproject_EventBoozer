// author: Dieter Daems
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class DataValidatieBasis : IDataErrorInfo
    {
        public string Error
        {
            get
            {
                string foutmeldingen = "";
                foreach (var item in this.GetType().GetProperties())
                {
                    string fout = this[item.Name];
                    if (!string.IsNullOrWhiteSpace(fout))
                    {
                        foutmeldingen += fout + Environment.NewLine;
                    }
                }
                return foutmeldingen;
            }
        }

        public bool IsGeldig()
        {
            return string.IsNullOrWhiteSpace(Error);
        }

        public virtual string this[string propertynaam]
        {
            get
            {
                return "";
            }
        }

        // TODO : plaats dit in een helper class
        public static bool IsValidTime(string thetime)
        {
            Regex checktime =
                new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

            return checktime.IsMatch(thetime);
        }
    }
}