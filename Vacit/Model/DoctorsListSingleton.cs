using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacit.Model
{
    public class DoctorsListSingleton :INotifyPropertyChanged
    {
        // Singleton instance
        private static DoctorsListSingleton instance;
        public static DoctorsListSingleton Instance
        {
            get
            {
                if (instance == null)
                { instance = new DoctorsListSingleton(); }
                return instance;
            }
        }




        //PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }





 

        // Entire doctorslist of all doctors in Denmark
        private ObservableCollection<Doctors> doctorsListTotal;
        public ObservableCollection<Doctors> DoctorsListTotal
        {
            get { return doctorsListTotal; }
            set { doctorsListTotal = value; }
        }


        // List only doctors by chosen postalcode, if none chosen then list all
        private ObservableCollection<Doctors> doctorsList;
        public ObservableCollection<Doctors> DoctorsList
        {
            get { return doctorsList; }
            set { doctorsList = value; OnPropertyChanged(nameof(DoctorsList)); }
        }






        public DoctorsListSingleton()
        {
            DoctorsList = new ObservableCollection<Doctors>();
            DoctorsListTotal = new ObservableCollection<Doctors>();

            GetDoctors();

            //Sort the list alfabetically - takes too long time!!!
            //SortDoctors();

            doctorsList = doctorsListTotal;            
        }



        private void GetDoctors()
        {
            doctorsListTotal = Persistency.PersistencyService.LoadDoctorsFromJsonAsync();
        }

        private void SortDoctors()
        {
            DoctorsListTotal = new ObservableCollection<Doctors>(doctorsListTotal.OrderBy(x => x.Praksisbetegnelse));

        }

        public void TrimListByPostalCode(int PostalCode)
        {
             DoctorsList = new ObservableCollection<Doctors>(doctorsListTotal.Where(x=>x.Postnr==PostalCode));
            
        }
    }
}
