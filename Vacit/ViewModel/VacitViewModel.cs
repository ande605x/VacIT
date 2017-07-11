using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vacit.Model;
using Vacit.Common;
using Vacit.Handler;

namespace Vacit.ViewModel
{
    public class VacitViewModel : INotifyPropertyChanged
    {

        // Singleton property
        public ChildrensListSingleton ChildrensListSingleton { get; set; }
        public VaccinesListSingleton VaccinesListSingleton { get; set; }


        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }




        //Add Child Properties
        private int childID;              // HVOR FÅR DEN ID FRA??????
        public int ChildID
        {
            get { return childID; }
            set { childID = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DateTimeOffset dateOfBirth;
        public DateTimeOffset DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

     
       


        private bool genderGirl;
        public bool GenderGirl
        {
            get { return genderGirl; }
            set { genderGirl = value; }
        }



        //Command Properties
        private ICommand createChildCommand;
        public ICommand CreateChildCommand  
        {
            get { return createChildCommand; }
            set { createChildCommand = value; }
        }

        private ICommand deleteChildCommand;
        public ICommand DeleteChildCommand
        {
            get { return deleteChildCommand; }
            set { deleteChildCommand = value; }
        }

        private ICommand updateChildCommand;
        public ICommand UpdateChildCommand
        {
            get { return updateChildCommand; }
            set { updateChildCommand = value; }
        }



        private Child selectedChild;
        public Child SelectedChild
        {
            get { return selectedChild; }
            set { selectedChild = value; OnPropertyChanged(nameof(SelectedChild)); }
        }



        private VaccinesCard selectedVaccineCard;

        public VaccinesCard SelectedVaccineCard
        {
            get { return selectedVaccineCard; }
            set { selectedVaccineCard = value;
                Common.ShowMessages.ShowPopUp("Valgt vaccinecard: "+value.VaccineName);
                OnPropertyChanged(nameof(SelectedVaccineCard)); }
        }




        //MIDLERTIDIG
        private int nextChildIDforView;

        public int NextChildIDforView
        {
            get { return nextChildIDforView; }
            set { nextChildIDforView = value; OnPropertyChanged(nameof(Name)); }
        }



        


        // Handler property
        public ChildHandler ChildHandler { get; set; }
        public VaccineHandler VaccineHandler { get; set; }




        // Constructor
        public VacitViewModel()
        {
            ChildrensListSingleton = ChildrensListSingleton.Instance;
            VaccinesListSingleton = VaccinesListSingleton.Instance;

            NextChildIDforView = ChildrensListSingleton.NextChildID;
            //ChildID = ChildrensListSingleton.NextChildID;

            ChildHandler = new Handler.ChildHandler(this);
            VaccineHandler = new Handler.VaccineHandler(this);

            DateTime dt = System.DateTime.Now; // Inilizing dateOfBirth to now as a standard 
            dateOfBirth = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, new TimeSpan());

            SelectedChild = ChildrensListSingleton.ChildrensList.First(); //Vælger barn nr 1 i listen til start
           

            CreateChildCommand = new RelayCommand(ChildHandler.CreateChild);
            DeleteChildCommand = new RelayCommand(ChildHandler.DeleteChild);
            UpdateChildCommand = new RelayCommand(ChildHandler.UpdateChild);



        }
    }
}
