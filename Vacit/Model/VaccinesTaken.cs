namespace Vacit.Model
{
    using System;
    using System.Collections.Generic;

    public class VaccinesTaken
    {
        public int ChildID { get; set; }
        public int VacMonthID { get; set; }
        public bool? VacTaken { get; set; }

        public VaccinesTaken()
        {

        }
    }
}
