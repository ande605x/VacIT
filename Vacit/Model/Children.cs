namespace Vacit.Model
{
    using System;
    using System.Collections.Generic;

    public class Children
    {


        public int ChildID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool GenderGirl { get; set; }

        public Children(string name, DateTime dateOfBirth, bool genderGirl)
        {
            // mangler
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.GenderGirl = genderGirl;
        }
    }
}
