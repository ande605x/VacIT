using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacit.ViewModel;
using Vacit.Model;
using Vacit.Converter;

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

            // Create new child object
            Child newChild = new Child(
                VacitViewModel.NextChildIDforView,
                //ChildID is automaticly made by database on server 
                //VIRKER IKKE - ChildID locally can needs to have the same number as on the server
                VacitViewModel.Name,
                DateConverter.DateTimeOffset_SetToDateTime(VacitViewModel.DateOfBirth),
                VacitViewModel.GenderGirl);

            // Add to list             
            VacitViewModel.ChildrensListSingleton.AddChild(newChild);







            // Create rows in VaccinesTaken for the new child

            foreach(var item in VaccinesListSingleton.Instance.MonthToTakeVaccinesList)
            {
                VaccinesTaken newVaccinesTaken = new VaccinesTaken(
                    VacitViewModel.NextChildIDforView,
                    item.VacMonthID);

                VaccinesListSingleton.Instance.VaccinesTakenList.Add(newVaccinesTaken);
            }


            // Increment next ChildID and make it ready for next post
            VacitViewModel.NextChildIDforView++;


        } 


        public void DeleteChild()
        {
            VacitViewModel.ChildrensListSingleton.RemoveChild(VacitViewModel.SelectedChild);

            // Husk slet vaccinestaken

        }

        public void UpdateChild()
        {
            VacitViewModel.ChildrensListSingleton.UpdateChild(VacitViewModel.SelectedChild);
        }


    }
}
