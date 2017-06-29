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



        //Command Properties
        private ICommand createChildCommand;
        public ICommand CreateChildCommand  
        {
            get { return createChildCommand; }
            set { createChildCommand = value; }
        }



        // Handler property
        public ChildHandler ChildHandler { get; set; }




        // Constructor
        public VacitViewModel()
        {
            ChildrensListSingleton = ChildrensListSingleton.Instance;

            DateTime dt = System.DateTime.Now; // Inilizing dateOfBirth to now as a standard 
            dateOfBirth = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, new TimeSpan());

            ChildHandler = new Handler.ChildHandler(this);
            CreateChildCommand = new RelayCommand(ChildHandler.CreateChild);

        }
    }
}
