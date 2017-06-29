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
            // create new child object
            Child newChild = new Child(
                                                //ChildID is automaticly made by database 
                VacitViewModel.Name,
                DateConverter.DateTimeOffset_SetToDateTime(VacitViewModel.DateOfBirth),
                VacitViewModel.GenderGirl);
            
            // add to list
            VacitViewModel.ChildrensListSingleton.AddChild(newChild);
        }


    }
}
