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
using System.Collections.ObjectModel;

namespace Vacit.ViewModel
{
    public class VacitViewModel : INotifyPropertyChanged
    {

        // Singleton properties
        public ChildrensListSingleton ChildrensListSingleton { get; set; }
        public VaccinesListSingleton VaccinesListSingleton { get; set; }
        public DoctorsListSingleton DoctorsListSingleton { get; set; }


        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }




        //Add Child Properties
        private int childID;            
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

        private bool genderBoy;
        public bool GenderBoy
        {
            get { return genderBoy; }
            set { genderBoy = value; }
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

  



        // For use in both AddChildView listview and ChildView combobox
        private Child selectedChild;
        public Child SelectedChild
        {
            get { return selectedChild; }
            set { selectedChild = value; OnPropertyChanged(nameof(SelectedChild));  }
        }


        // Used in ChildView gridview
        private VaccinesCard selectedVaccineCard;
        public VaccinesCard SelectedVaccineCard
        {
            get { return selectedVaccineCard; }
            set
            {
                selectedVaccineCard = value;
                if (LastChangedCard!=selectedVaccineCard && value!=null)
                {
                    LastChangedCard = value;
                    ChildHandler.UpdateVaccineTaken();
                }
            }
        }

 
        // When selecting card to change VaccineTaken status, make sure not just selected (secure no stackoverflow happens going in circles because SelectedVaccineCard stays selected)
        public VaccinesCard LastChangedCard { get; set; }





        // Used in VaccinesInfoView combobox
        private Vaccine selectedVaccine;
        public Vaccine SelectedVaccine
        {
            get { return selectedVaccine; }
            set { selectedVaccine = value; OnPropertyChanged(nameof(SelectedVaccine)); }
        }



        // Used in DoctorsView listview 
        private Doctors selectedDoctor;
        public Doctors SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value; OnPropertyChanged(nameof(SelectedDoctor));
                if (SelectedDoctor != null)
                {
                    if (SelectedDoctor.System.Contains("A-Data")) DoctorsSystemURL = "http://www.aftalebogen.dk";
                    else if (SelectedDoctor.System.Contains("NOVAX")) DoctorsSystemURL = "https://www.laegevejen.dk";
                    else DoctorsSystemURL = "https://www.sundhed.dk/borger/guides/find-behandler/?Informationskategori=Praktiserende%20l%C3%A6ge";
                }
            }
        }


        // Used in DoctorsView for online booking webview
        private string doctorsSystemURL;
        public string DoctorsSystemURL
        {
            get { return doctorsSystemURL; }
            set { doctorsSystemURL = value; OnPropertyChanged(nameof(DoctorsSystemURL)); }
        }




        // Used in DoctorsView to show only the doctors in the chosen postal code area
        private int doctorsPostalCodeChosen;
        public int DoctorsPostalCodeChosen
        {
            get { return doctorsPostalCodeChosen; }
            set {   doctorsPostalCodeChosen = value;
                    OnPropertyChanged(nameof(DoctorsPostalCodeChosen));
                    if (doctorsPostalCodeChosen > 900 && doctorsPostalCodeChosen < 10000)
                    {
                        DoctorsListSingleton.DoctorsList = new ObservableCollection<Doctors>(DoctorsListSingleton.DoctorsListTotal.Where(x => x.Postnr == doctorsPostalCodeChosen));
                    }
                    else
                    {
                         DoctorsListSingleton.DoctorsList = DoctorsListSingleton.DoctorsListTotal;
                    }
                }
        }





        // ChildID is autogenerated by SQL Identity. This is for keeping track of the ID when adding a new child and making the VaccinesTaken rows for the new child
        public int nextChildID { get; set; }



        // Handler property
        public ChildHandler ChildHandler { get; set; }









        // Constructor
        public VacitViewModel()
        {
            ChildrensListSingleton = ChildrensListSingleton.Instance;
            VaccinesListSingleton = VaccinesListSingleton.Instance;
            DoctorsListSingleton = DoctorsListSingleton.Instance;

            ChildHandler = new Handler.ChildHandler(this);

            DateTime dt = System.DateTime.Now; // Inilizing dateOfBirth to now as a standard 
            dateOfBirth = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, new TimeSpan());

            SelectedChild = ChildrensListSingleton.ChildrensList.First(); //Selecting child number 1 as i start

            GenderBoy = false;
            GenderGirl = false;

            LastChangedCard = null;
            
           
            CreateChildCommand = new RelayCommand(ChildHandler.CreateChild);
            DeleteChildCommand = new RelayCommand(ChildHandler.DeleteChild,IsChildListNotEmpty);
     
        }



        public bool IsChildListNotEmpty()
        {
            if (ChildrensListSingleton.ChildrensList.Count() > 0)
                return true;
            else
                return false;
        }


    }
}
