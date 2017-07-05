using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacit.Persistency;

namespace Vacit.Model
{
    public class VaccinesListSingleton
    {
        // Singleton instance
        private static VaccinesListSingleton instance;
        public static VaccinesListSingleton Instance
        {
            get
            {
                if (instance == null)
                { instance = new VaccinesListSingleton(); }
                return instance;
            }
        }




        private ObservableCollection<Vaccine> vaccinesList;
        public ObservableCollection<Vaccine> VaccinesList
        {
            get { return vaccinesList; }
            set { vaccinesList = value; }
        }

        private ObservableCollection<VaccinesWithMonths> vaccinesWithMonthList;

        public ObservableCollection<VaccinesWithMonths> VaccinesWithMonthList
        {
            get { return vaccinesWithMonthList; }
            set { vaccinesWithMonthList = value; }
        }



        public PersistencyService ps;



        //Constructor
        public VaccinesListSingleton()
        {
            VaccinesList = new ObservableCollection<Vaccine>();
            VaccinesWithMonthList = new ObservableCollection<VaccinesWithMonths>();
            ps = new PersistencyService();

            // Get list from database from server
            GetVaccines();

        }



        // Metoder
        public void GetVaccines()
        {
            vaccinesList = PersistencyService.LoadVaccinesFromJsonAsync();   // SKAL BEGGE BRUGES????
            vaccinesWithMonthList = PersistencyService.LoadVaccinesWithMonthsFromJsonAsync();        }

        

    }
}
