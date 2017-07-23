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




        private int nextChildID;

        public int NextChildID
        {
            get { return nextChildID; }
            set { nextChildID = value; }
        }






        public CardList CardListForView { get; set; }












        //Constructor
        public ChildrensListSingleton()
        {
            ChildrensList = new ObservableCollection<Child>();
            ps = new PersistencyService();

                //Testdata
                //ChildrensList.Add(new Child("Anna",System.DateTime.Now, true));
                //ChildrensList.Add(new Child("Jens", System.DateTime.Now, false));

            // Get list from database from server
            
            GetChildren();


            //nextChildID= childrensList.Last().ChildID+1;

            // Find ChildID for the next new child (last ChildID+1)  (This only run once when app opens)
            foreach (var i in childrensList)
            {
                nextChildID = i.ChildID;
            }
            nextChildID++;

        }



        // Metoder
        public void GetChildren()
        {
            childrensList = PersistencyService.LoadChildrenFromJsonAsync();
        }


        public void AddChild(Child newChild)
        {
            ChildrensList.Add(newChild);                       // Add child locally
            PersistencyService.SaveChildAsJsonAsync(newChild); // Add child to server



            foreach (var item in VaccinesListSingleton.Instance.MonthToTakeVaccinesList)
            {
                VaccinesTaken newVaccinesTaken = new VaccinesTaken(
                   NextChildID,
                   item.VacMonthID);

                Persistency.PersistencyService.SaveVaccinesTakenAsJsonAsync(newVaccinesTaken);

            }




            // Make ToastNotifications
            foreach (var vwmlItem in VaccinesListSingleton.Instance.VaccinesWithMonthList)
            {
               
                DateTime vacTime = newChild.DateOfBirth.AddMonths(vwmlItem.MonthToTake);
                if (vacTime >= DateTime.Now)  // Only notify if date is after present time
                {
                    ToastMessages.CreateToastMessage("Husk at booke tid til vaccination:",
                                                     newChild.Name + " skal vaccineres " + Vacit.Converter.DateConverter.DateStringDanish(vacTime),       //+ " er " + newChild.AgeStringDanish + " gammel, og derfor er der " + " til",
                                                     "Vaccine: " + vwmlItem.VacName,
                                                     @"C:\SOURCE\Vacit\Vacit\Assets\beskyt_mod_vaccination.jpg",
                                                     vacTime);
                }

            }



            // Create rows in VaccinesTaken for the new child
            //foreach (var item in VaccinesListSingleton.Instance.MonthToTakeVaccinesList)
            //{
            //    vt = new VaccinesTaken(newChild.ChildID, item.VacMonthID);
            //}
            ////PersistencyService

            //// LINQ combinding 3 lists
            //var standardCardList = from v in VaccinesListSingleton.Instance.VaccinesList
            //                       join vwm in VaccinesListSingleton.Instance.MonthToTakeVaccinesList
            //                       on v.VacID equals vwm.VacID                      
            //                       orderby vwm.MonthToTake
            //                       select new CardList() { vaccineName = v.VacName, monthToTake = vwm.MonthToTake };

            //CardListForView = new CardList();
            //c.VaccinesCardList = new ObservableCollection<VaccinesCard>();

            ////numberOfCards = VaccinesListSingleton.Instance.MonthToTakeVaccinesList.Count;

            //foreach (var item in standardCardList)
            //{
            //     c.VaccinesCardList.Add(new VaccinesCard(item.vaccineName, item.monthToTake, false, newChild.GenderGirl));
            //}


            nextChildID++;                                     // Increment nextChildID
        }

        public void RemoveChild(Child childToRemove)
        {
            ChildrensList.Remove(childToRemove);                        // Delete child locally
            PersistencyService.DeleteChildFromJsonAsync(childToRemove); // Delete child from server
        }

        public void UpdateChild(Child childToUpdate)
        {
            ChildrensList.Clear();  // Clear list ????????????
            PersistencyService.UpdateChildJsonAsync(childToUpdate);
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

            if (true)//foundTaken != null)
            {
                if (foundTaken.VacTaken) foundTaken.VacTaken = false;
                else foundTaken.VacTaken = true;
            }
            //VaccinesListSingleton.Instance.VaccinesTakenList.CollectionChanged += VaccinesTakenList_CollectionChanged;

            

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


            //VaccinesListSingleton.Instance.VaccinesCardList.Insert(vaccineCardToUpdateIndex,vaccineCardToUpdate);



            PersistencyService.UpdateVaccinesTakenJsonAsync(foundTaken);
        }

        
    }
}
