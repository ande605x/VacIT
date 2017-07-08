namespace Vacit.Model
{
    using System;
    using System.Collections.Generic;

    public class VaccinesTaken
    {
        public int ChildID { get; set; }
        public int VacMonthID { get; set; }
        public bool VacTaken { get; set; }   // tidligere "bool?"

        public VaccinesTaken(int childID, int vacMonthID)
        {
            this.ChildID = childID;
            this.VacMonthID = vacMonthID;
            VacTaken = false; // At construction the default is that no vaccines is taken
        }
    }
}
