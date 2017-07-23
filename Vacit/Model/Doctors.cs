namespace Vacit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Doctors
    {
        public int Ydertype { get; set; }

        public double? Speciale { get; set; }

       
        public int Ydernr { get; set; }  //Primary Key

        public string titelpost { get; set; }

        public string System { get; set; }

       
        public string Praksisbetegnelse { get; set; }

       
        public string Adresse { get; set; }

        public int Postnr { get; set; }

        
        public string Post_navn { get; set; }

        public double? Amt { get; set; }

        
        public string navn { get; set; }

        public double? Lokationsnr { get; set; }

        public int? Telefonnr_1 { get; set; }


        public Doctors()
        {

        }
    }
}
