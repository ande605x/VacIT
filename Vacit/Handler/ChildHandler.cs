using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacit.ViewModel;
using Vacit.Model;
using Vacit.Converter;
using Vacit.Common;

namespace Vacit.Handler
{
    public class ChildHandler
    {
        public VacitViewModel VacitViewModel { get; set; }

        //Constructor
        public ChildHandler(VacitViewModel vacitViewModel)
        {
            VacitViewModel = vacitViewModel;
        }



        public void CreateChild()
        {
            try
            { 
                if (VacitViewModel.DateOfBirth > DateTime.Now)
                    throw new ArgumentException("Fødselsdato valgt er i fremtiden. Vælg en anden dato!");
                if (VacitViewModel.Name == null || VacitViewModel.Name == "" || VacitViewModel.Name.Contains(" "))
                    throw new ArgumentException("Navn ikke indtastet eller indeholder mellemrum.");
                 if (!VacitViewModel.GenderGirl && !VacitViewModel.GenderBoy)
                    throw new ArgumentException("Kønnet ikke valgt.");


                // Because childID is sql identity auto increment:
                // To get next child ID by adding dummy child and deleting it again
                // Next Child ID will then by the dummy childID + 1
                Persistency.PersistencyService.SaveChildAsJsonAsync(new Child(0,"Dummy",DateTime.Now,false));
                var childrensDummyList = Persistency.PersistencyService.LoadChildrenFromJsonAsync();
                var dummyChild = childrensDummyList.FirstOrDefault(x => x.Name.Contains("Dummy"));
                Persistency.PersistencyService.DeleteChildFromJsonAsync(dummyChild);
                VacitViewModel.nextChildID = dummyChild.ChildID + 1;
                // Remarks: This way every second child ID is deleted and falls out of the list, but it was the only way I could solve the identity problem.
                //          It is also not suited for multiple users if another user makes a record after me getting the ID and before I make my record in the list. 



                // Create rows in VaccinesTaken for the new child
                foreach (var item in VaccinesListSingleton.Instance.MonthToTakeVaccinesList)
                {

                    // Find vaccinenames with contains the word "piger"
                    var containsPiger = VaccinesListSingleton.Instance.VaccinesList.FirstOrDefault(x => x.VacName.Contains("piger"));
                    // Test if boy and contains "piger" OR is a girl  
                    if ((!VacitViewModel.GenderGirl && containsPiger.VacID != item.VacID || VacitViewModel.GenderGirl))
                    {
                        VaccinesTaken newVaccinesTaken = new VaccinesTaken(
                            VacitViewModel.nextChildID,
                            item.VacMonthID); // VaccineTaken is false when creating new child

                        VaccinesListSingleton.Instance.VaccinesTakenList.Add(newVaccinesTaken);
                    }
                }



                // Create new child object
                Child newChild = new Child(
                    VacitViewModel.nextChildID,  //ChildID is in fact automaticly made by database on server 
                    VacitViewModel.Name,
                    DateConverter.DateTimeOffset_SetToDateTime(VacitViewModel.DateOfBirth),
                    VacitViewModel.GenderGirl);

                // Add to list             
                VacitViewModel.ChildrensListSingleton.AddChild(newChild);         
            }
            catch (Exception ex)
            {
                ShowMessages.ShowPopUp("Nyt barn IKKE oprettet:\n" + ex.Message);
            }
            finally
            {
            }


        } 


        public void DeleteChild()
        {
            VacitViewModel.ChildrensListSingleton.RemoveChild(VacitViewModel.SelectedChild);
        }

 



        public void UpdateDoctorsList()
        {
            VacitViewModel.DoctorsListSingleton.TrimListByPostalCode(VacitViewModel.DoctorsPostalCodeChosen);
        }




        public void UpdateVaccineTaken()
        {
            int indx = VacitViewModel.SelectedChild.VaccinesCardList.IndexOf(VacitViewModel.SelectedVaccineCard);
            VacitViewModel.ChildrensListSingleton.UpdateVaccineTaken(VacitViewModel.SelectedVaccineCard,
                                                                     indx,
                                                                     VacitViewModel.SelectedChild.ChildID
                                                                     );
            var tempCard = VacitViewModel.SelectedVaccineCard;
            VacitViewModel.SelectedChild.VaccinesCardList.RemoveAt(indx);
            VacitViewModel.SelectedChild.VaccinesCardList.Insert(indx, tempCard);
        }

    }
}
