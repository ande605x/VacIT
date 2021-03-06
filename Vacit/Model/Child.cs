namespace Vacit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Child
    {

        // Properties from database
        public int ChildID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool GenderGirl { get; set; }



        // Properties calculated
        public string GenderColor { get; set; }

        public string DateOfBirthStringDanish { get; set; }

        public string AgeStringDanish { get; set; }



       
        // Making one VaccinesCardList for every child
        private ObservableCollection<VaccinesCard> vaccinesCardList;
        public ObservableCollection<VaccinesCard> VaccinesCardList
        {
            get { return vaccinesCardList; }
            set { vaccinesCardList = value; }
        }






        public Child(int childID,string name, DateTime dateOfBirth, bool genderGirl)
        {
            this.ChildID = childID;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.GenderGirl = genderGirl;

            if (genderGirl) GenderColor = "#FFB94CA6"; else GenderColor = "#FF006C95";     //could also write GenderColor="Blue"

            DateOfBirthStringDanish = Vacit.Converter.DateConverter.DateStringDanish(dateOfBirth);

            AgeStringDanish = Vacit.Converter.DateConverter.AgeToStringDanish(dateOfBirth);




            // LINQ combinding 3 lists, making the cards in ChildView where we need the 3 propetiers: vaccineName, monthToTake and taken
            var cardList = from vt in VaccinesListSingleton.Instance.VaccinesTakenList
                           join vwm in VaccinesListSingleton.Instance.MonthToTakeVaccinesList
                           on vt.VacMonthID equals vwm.VacMonthID
                           join v in VaccinesListSingleton.Instance.VaccinesList
                           on vwm.VacID equals v.VacID
                           where vt.ChildID == childID
                           orderby vwm.MonthToTake
                           select new {vaccineName=v.VacName, monthToTake = vwm.MonthToTake, taken = vt.VacTaken };

            VaccinesCardList = new ObservableCollection<VaccinesCard>();

            foreach (var item in cardList)
            {
                VaccinesCardList.Add(new VaccinesCard(item.vaccineName, item.monthToTake, item.taken, genderGirl, dateOfBirth));
            }
        }
    }
}
