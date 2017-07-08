namespace Vacit.Model
{
    using System;
    using System.Collections.Generic;

    public class Child
    {


        public int ChildID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool GenderGirl { get; set; }




        public string GenderColor { get; set; }

        public string DateOfBirthStringDanish { get; set; }

        public string AgeStringDanish { get; set; }

       


        public Child(int childID,string name, DateTime dateOfBirth, bool genderGirl)
        {
            // mangler id ???
            this.ChildID = childID;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.GenderGirl = genderGirl;

            if (genderGirl) GenderColor = "#FFB94CA6"; else GenderColor = "#FF006C95";     //could also write GenderColor="Blue"

            DateOfBirthStringDanish = Vacit.Converter.DateConverter.DateStringDanish(dateOfBirth);

            AgeStringDanish = Vacit.Converter.DateConverter.AgeToStringDanish(dateOfBirth);
        }
    }
}
