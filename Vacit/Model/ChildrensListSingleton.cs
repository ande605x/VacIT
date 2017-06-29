using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        //Constructor
        public ChildrensListSingleton()
        {
            ChildrensList = new ObservableCollection<Child>();

            //testdata - TJEK AT MAN KAN GØRE DET SÅDAN HER UDEN {}
            ChildrensList.Add(new Child("Anna",System.DateTime.Now, true));
            ChildrensList.Add(new Child("Jens", System.DateTime.Now, false));
            
            // hent fra databasen fra server

        }



        // Metoder
        public void AddChild(Child newChild)
        {
            ChildrensList.Add(newChild);
            // save list to server
        }




    }
}
