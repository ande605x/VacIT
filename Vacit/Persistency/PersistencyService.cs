using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vacit.Common;
using Vacit.Model;

namespace Vacit.Persistency
{
    public class PersistencyService
    {

        public ShowMessages ShowMessage;

        const string serverUrl = "http://vacitwebservice.azurewebsites.net";
        const string urlChildren = "api/children/";



        //GET Child
        public static ObservableCollection<Child> LoadChildrenFromJsonAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.GetAsync(urlChildren).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var childrensList = response.Content.ReadAsAsync<ObservableCollection<Child>>().Result;
                        return childrensList;
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Netværksfejl: " + e.Message);
                
            }
            return null; // If error, return null
        }




        // POST Child
        public static void SaveChildAsJsonAsync(Child newChildToAdd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.PostAsJsonAsync<Child>(urlChildren, newChildToAdd).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        ShowMessages.ShowPopUp("Fejl. Barnet blev ikke oprettet i databasen: " + response.StatusCode);
                    }

                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Netværksfejl: " + e.Message);
            }
        }




        //DELETE Child
        public static void DeleteChildFromJsonAsync(Child selectedChildToDelete)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Clear();
                    string urlDelete = urlChildren + selectedChildToDelete.ChildID;

                    HttpResponseMessage response = client.DeleteAsync(urlDelete).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        ShowMessages.ShowPopUp("Fejl. Barnet blev ikke slettet fra databasen: " + response.StatusCode);
                    }
                }

            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Netværksfejl: " + e.Message);
            }
        }




        // PUT Child
        public static void UpdateChildJsonAsync(Child selectedChildToUpdate)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Clear();
                    string urlUpdate = urlChildren + selectedChildToUpdate.ChildID;

                    HttpResponseMessage response = client.PutAsJsonAsync<Child>(urlUpdate,selectedChildToUpdate).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        ShowMessages.ShowPopUp("Fejl. Barnet blev ikke opdateret i databasen: " + response.StatusCode);
                    }
                    else
                    {
                        ShowMessages.ShowPopUp("Barnet er nu opdateret i databasen");
                    }
                }

            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Netværksfejl: " + e.Message);
            }
        }
        


            

   






        // Constructor
        public PersistencyService()
        {
            ShowMessage = new ShowMessages();

        }
    }
}
