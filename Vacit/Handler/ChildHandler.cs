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



        // HUSK TRY CATCH
        public void CreateChild()
        {
           

            try
            {
                VacitViewModel.IsBusy = true;

                if (VacitViewModel.DateOfBirth > DateTime.Now)
                    throw new ArgumentException("Fødselsdato valgt er i fremtiden. Vælg en anden dato!");
                if (VacitViewModel.Name == null || VacitViewModel.Name == "" || VacitViewModel.Name.Contains(" "))
                    throw new ArgumentException("Navn ikke indtastet eller indeholder mellemrum.");
                 if (!VacitViewModel.GenderGirl && !VacitViewModel.GenderBoy)
                    throw new ArgumentException("Kønnet ikke valgt.");



                // Create rows in VaccinesTaken for the new child

                foreach (var item in VaccinesListSingleton.Instance.MonthToTakeVaccinesList)
                {

                    // Find vaccinenames with contains the word "piger"
                    var containsPiger = VaccinesListSingleton.Instance.VaccinesList.FirstOrDefault(x => x.VacName.Contains("piger"));
                    // Test if boy and contains "piger" OR is a girl  
                    if ((!VacitViewModel.GenderGirl && containsPiger.VacID != item.VacID || VacitViewModel.GenderGirl))
                    {
                        VaccinesTaken newVaccinesTaken = new VaccinesTaken(
                            VacitViewModel.NextChildIDforView,
                            item.VacMonthID); // VaccineTaken is false when creating new child


                        VaccinesListSingleton.Instance.VaccinesTakenList.Add(newVaccinesTaken);
                    }
                    //Persistency.PersistencyService.SaveVaccinesTakenAsJsonAsync(newVaccinesTaken);

                }


          
                // Create new child object
                Child newChild = new Child(
                    VacitViewModel.NextChildIDforView,
                    //ChildID is automaticly made by database on server 
                    //VIRKER IKKE - ChildID locally needs to have the same number as on the server
                    VacitViewModel.Name,
                    DateConverter.DateTimeOffset_SetToDateTime(VacitViewModel.DateOfBirth),
                    VacitViewModel.GenderGirl);

                // Add to list             
                VacitViewModel.ChildrensListSingleton.AddChild(newChild);
         


                // Increment next ChildID and make it ready for next post
                VacitViewModel.NextChildIDforView++;

               
            }

            catch (Exception ex)
            {
                ShowMessages.ShowPopUp("Nyt barn IKKE oprettet:\n" + ex.Message);
            }
            finally
            {
                VacitViewModel.IsBusy = false;
            }


        } 


        public void DeleteChild()
        {
            VacitViewModel.ChildrensListSingleton.RemoveChild(VacitViewModel.SelectedChild);
            // slet vaccine taken er inkluderet i ovenstående
  

        }

        public void UpdateChild()
        {
            VacitViewModel.ChildrensListSingleton.UpdateChild(VacitViewModel.SelectedChild);
        }




        public void UpdateDoctorsList()
        {
            VacitViewModel.DoctorsListSingleton.TrimListByPostalCode(VacitViewModel.DoctorsPostalCodeChosen);
        }



        public void UpdateFromButton()
        {
            foreach (var item in VacitViewModel.SelectedChild.VaccinesCardList)
            {

                int indx = VacitViewModel.SelectedChild.VaccinesCardList.IndexOf(item);
                VacitViewModel.ChildrensListSingleton.UpdateVaccineTaken(item,indx,VacitViewModel.SelectedChild.ChildID);
                                                                         
                var tempCard = item;
                VacitViewModel.SelectedChild.VaccinesCardList.RemoveAt(indx);//VacitViewModel.SelectedVaccineCard);
                                                                             //if (tempCard.Taken) tempCard.Taken = false; else tempCard.Taken = true;
                VacitViewModel.SelectedChild.VaccinesCardList.Insert(indx, tempCard);
            }

        }




        public void UpdateVaccineTaken()
        {
            int indx = VacitViewModel.SelectedChild.VaccinesCardList.IndexOf(VacitViewModel.SelectedVaccineCard);
            VacitViewModel.ChildrensListSingleton.UpdateVaccineTaken(VacitViewModel.SelectedVaccineCard,
                                                                     indx,
                                                                     VacitViewModel.SelectedChild.ChildID
                                                                     );
            var tempCard = VacitViewModel.SelectedVaccineCard;
            VacitViewModel.SelectedChild.VaccinesCardList.RemoveAt(indx);//VacitViewModel.SelectedVaccineCard);
            //if (tempCard.Taken) tempCard.Taken = false; else tempCard.Taken = true;
            VacitViewModel.SelectedChild.VaccinesCardList.Insert(indx, tempCard);


            
            //VacitViewModel.SelectedVaccineCard.Taken = false;
            //VacitViewModel.SelectedChild.VaccinesCardList[indx]= VacitViewModel.SelectedVaccineCard;
            //VacitViewModel.SelectedChild.VaccinesCardList.Remove(VacitViewModel.SelectedVaccineCard);
        }

    }
}
