//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ToDo
    {
        public int ToDoID { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public bool Afgewerkt { get; set; }
        public int Volgnr { get; set; }
        public int EventID { get; set; }
    
        public virtual Event Event { get; set; }
    }
}