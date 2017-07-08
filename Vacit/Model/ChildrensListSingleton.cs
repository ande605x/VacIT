using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacit.Persistency;

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



        public PersistencyService ps;






        private int nextChildID;

        public int NextChildID
        {
            get { return nextChildID; }
            set { nextChildID = value; }
        }








        //Constructor
        public ChildrensListSingleton()
        {
            ChildrensList = new ObservableCollection<Child>();
            ps = new PersistencyService();

                //Testdata
                //ChildrensList.Add(new Child("Anna",System.DateTime.Now, true));
                //ChildrensList.Add(new Child("Jens", System.DateTime.Now, false));

            // Get list from database from server
            GetChildren();


            //nextChildID= childrensList.Last().ChildID+1;

            // Find ChildID for the next new child (last ChildID+1)  (This only run once when app opens)
            foreach (var i in childrensList)
            {
                nextChildID = i.ChildID;
            }
            nextChildID++;

        }



        // Metoder
        public void GetChildren()
        {
            childrensList = PersistencyService.LoadChildrenFromJsonAsync();
        }


        public void AddChild(Child newChild)
        {
            ChildrensList.Add(newChild);                       // Add child locally
            PersistencyService.SaveChildAsJsonAsync(newChild); // Add child to server
            nextChildID++;                                     // Increment nextChildID

            // Create rows in VaccinesTaken for the new child
        

        }

        public void RemoveChild(Child childToRemove)
        {
            ChildrensList.Remove(childToRemove);                        // Delete child locally
            PersistencyService.DeleteChildFromJsonAsync(childToRemove); // Delete child from server
        }

        public void UpdateChild(Child childToUpdate)
        {
            ChildrensList.Clear();  // Clear list ????????????
            PersistencyService.UpdateChildJsonAsync(childToUpdate);
        }




    }
}
