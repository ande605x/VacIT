using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacit.Persistency;
using Vacit.Common;

namespace Vacit.Model
{
    public class ChildrensListSingleton
    {

        // Singleton instance
        private static ChildrensListSingleton instance;
        public static ChildrensListSingleton Instance
        {
            get {
                if (instance == null)
                    { instance = new ChildrensListSingleton(); }
                return instance;
                }
        }

        


        private ObservableCollection<Child> childrensList;
        public ObservableCollection<Child> ChildrensList
        {
            get { return childrensList; }
            set { childrensList = value; }
        }



        public PersistencyService ps;

        public VaccinesTaken vt;

        public Child c;






        
        public int nextChildID2 { get; set; }

        










        //Constructor
        public ChildrensListSingleton()
        {
            ChildrensList = new ObservableCollection<Child>();
            ps = new PersistencyService();

            // Get list from database from server
            GetChildren();
        }



        // Metoder
        public void GetChildren()
        {
            childrensList = PersistencyService.LoadChildrenFromJsonAsync();
        }


        public void AddChild(Child newChild)
        {
            nextChildID2 = newChild.ChildID;

            ChildrensList.Add(newChild);                       // Add child locally
            PersistencyService.SaveChildAsJsonAsync(newChild); // Add child to server


            // Create rows in VaccinesTaken for the new child
            foreach (var item in VaccinesListSingleton.Instance.MonthToTakeVaccinesList)
            {
                // Find vaccinenames with contains the word "piger" using predicate
                var containsPiger = VaccinesListSingleton.Instance.VaccinesList.FirstOrDefault(x => x.VacName.Contains("piger"));
                // Test if boy and contains "piger" OR is a girl  
                if ((!newChild.GenderGirl && containsPiger.VacID != item.VacID || newChild.GenderGirl))
                {
                   VaccinesTaken newVaccinesTaken = new VaccinesTaken(
                   nextChildID2,
                   item.VacMonthID); // VaccineTaken is false when creating new child

                   Persistency.PersistencyService.SaveVaccinesTakenAsJsonAsync(newVaccinesTaken);
                }

            }




            // Make ToastNotifications
            foreach (var vwmlItem in VaccinesListSingleton.Instance.VaccinesWithMonthList)
            {
                // Find vaccinenames with contains the word "piger"
                var containsPiger = VaccinesListSingleton.Instance.VaccinesList.FirstOrDefault(x => x.VacName.Contains("piger"));
                // Test if boy and contains "piger" OR is a girl  
                if ((!newChild.GenderGirl && containsPiger.VacID != vwmlItem.VacID || newChild.GenderGirl))
                {

                    DateTime vacTime = newChild.DateOfBirth.AddMonths(vwmlItem.MonthToTake);
                    if (vacTime >= DateTime.Now)  // Only notify if date is after present time
                    {
                        ToastMessages.CreateToastMessage("Husk at booke tid til vaccination:",
                                                         newChild.Name + " skal vaccineres senest " + Vacit.Converter.DateConverter.DateStringDanish(vacTime),
                                                         "Vaccine: " + vwmlItem.VacName,
                                                         @"C:\SOURCE\Vacit\Vacit\Assets\beskyt_mod_vaccination.jpg",
                                                         vacTime);
                    }
                }
            }                
        }


        public void RemoveChild(Child childToRemove)
        {
            // Remove from VaccinesTakenList with right ChildID
            var countTaken = VaccinesListSingleton.Instance.VaccinesTakenList.Count(x => x.ChildID == childToRemove.ChildID);
            for (int i=0;i<=countTaken;i++)
            {
                var firstItem = VaccinesListSingleton.Instance.VaccinesTakenList.FirstOrDefault(x => x.ChildID == childToRemove.ChildID);
                if (firstItem != null)
                { 
                        VaccinesListSingleton.Instance.VaccinesTakenList.Remove(firstItem);
                        Persistency.PersistencyService.DeleteVaccinesTakenJsonAsync(firstItem);
                }
             }


            ChildrensList.Remove(childToRemove);                        // Delete child locally
            PersistencyService.DeleteChildFromJsonAsync(childToRemove); // Delete child from server

            // Missing: Removeing ToastMessages here

        }



        public void UpdateVaccineTaken(VaccinesCard vaccineCardToUpdate,int vaccineCardToUpdateIndex, int childIDKey)
        {

            // Find VacMonthID of selected card via VaccineName (to find VacID) and MonthToTake
            int cardVacID = VaccinesListSingleton.Instance.VaccinesList.FirstOrDefault
                                (x => x.VacName == vaccineCardToUpdate.VaccineName)
                                .VacID;

            int vacMonthIDKey = VaccinesListSingleton.Instance.MonthToTakeVaccinesList.FirstOrDefault
                                    (x=>x.VacID==cardVacID && x.MonthToTake==vaccineCardToUpdate.MonthToTake)
                                    .VacMonthID;

            
            
            // Using lambda expression to find VaccineTaken with the composite keys
            var foundTaken = VaccinesListSingleton.Instance.VaccinesTakenList.FirstOrDefault(x=>x.ChildID == childIDKey && x.VacMonthID==vacMonthIDKey);

            if (foundTaken.VacTaken) foundTaken.VacTaken = false;
            else foundTaken.VacTaken = true;
                        
            if (vaccineCardToUpdate.Taken) vaccineCardToUpdate.Taken = false;
            else vaccineCardToUpdate.Taken = true;




            bool genderGirl = ChildrensListSingleton.Instance.ChildrensList.FirstOrDefault
                                  (x => x.ChildID == childIDKey)
                                  .GenderGirl;

            if (vaccineCardToUpdate.Taken)
            {
                vaccineCardToUpdate.CardColor = "#7F444444";
                vaccineCardToUpdate.AgeIconOpacity = 0.5;
            }
            else
            {
                if (genderGirl) vaccineCardToUpdate.CardColor = "#CCB94CA6";
                else vaccineCardToUpdate.CardColor = "#CC006C95";    
                vaccineCardToUpdate.AgeIconOpacity = 1.0;
            }





            DateTime dateOfBirth = ChildrensListSingleton.Instance.ChildrensList.FirstOrDefault
                                  (x => x.ChildID == childIDKey)
                                  .DateOfBirth;

            int monthToTake = vaccineCardToUpdate.MonthToTake;


            if (vaccineCardToUpdate.Taken) vaccineCardToUpdate.WhenToTakeStringDanish = "\nDu har markeret vaccine taget";
            else
            {
                if (dateOfBirth.AddMonths(monthToTake) <= DateTime.Now)
                    vaccineCardToUpdate.WhenToTakeStringDanish = "Vaccine skulle være taget for\n" + Converter.DateConverter.AgeToStringDanish(dateOfBirth.AddMonths(monthToTake)) + " siden";
                else
                    vaccineCardToUpdate.WhenToTakeStringDanish = "Vaccine skal tages om\n" + Converter.DateConverter.AgeToStringDanish(dateOfBirth.AddMonths(monthToTake));
            }




            PersistencyService.UpdateVaccinesTakenJsonAsync(foundTaken);
        }

        
    }
}
