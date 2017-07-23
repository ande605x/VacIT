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



        //GET Children
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
                ShowMessages.ShowPopUp("Fejl: " + e.Message);
                
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
                ShowMessages.ShowPopUp("Fejl: " + e.Message);
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
                ShowMessages.ShowPopUp("Fejl: " + e.Message);
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
                ShowMessages.ShowPopUp("Fejl: " + e.Message);
            }
        }















        const string urlVaccines = "api/vaccines/"; 

        //GET Vaccines
        public static ObservableCollection<Vaccine> LoadVaccinesFromJsonAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.GetAsync(urlVaccines).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var vaccinesList = response.Content.ReadAsAsync<ObservableCollection<Vaccine>>().Result;
                        return vaccinesList;
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Fejl: " + e.Message);

            }
            return null; // If error, return null
        }










        
        const string urlVaccinesWithMonth = "api/VaccinesWithMonthsViews/";

        //GET VaccinesWithMonths
        public static ObservableCollection<VaccinesWithMonths> LoadVaccinesWithMonthsFromJsonAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.GetAsync(urlVaccinesWithMonth).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var vaccinesList = response.Content.ReadAsAsync<ObservableCollection<VaccinesWithMonths>>().Result;
                        return vaccinesList;
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Fejl: " + e.Message);

            }
            return null; // If error, return null
        }




        const string urlVaccinesTaken = "api/VaccinesTakens";

        //GET VaccinesTaken
        public static ObservableCollection<VaccinesTaken> LoadVaccinesTakenFromJsonAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.GetAsync(urlVaccinesTaken).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var vaccinesTakenList = response.Content.ReadAsAsync<ObservableCollection<VaccinesTaken>>().Result;
                        return vaccinesTakenList;
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Fejl: " + e.Message);

            }
            return null; // If error, return null
        }



        // POST VaccinsTaken
        public static void SaveVaccinesTakenAsJsonAsync(VaccinesTaken newVaccineTakenToAdd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.PostAsJsonAsync<VaccinesTaken>(urlVaccinesTaken, newVaccineTakenToAdd).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        ShowMessages.ShowPopUp("Fejl. VaccineTaget blev ikke oprettet i databasen: " + response.StatusCode);
                    }

                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Fejl: " + e.Message);
            }
        }



        // PUT VaccinesTaken
        public static void UpdateVaccinesTakenJsonAsync(VaccinesTaken selectedVaccineTakenToUpdate)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Clear();
                    string urlUpdateVT = urlVaccinesTaken+"?ChildID="+ selectedVaccineTakenToUpdate.ChildID+"&VacMonthID="+selectedVaccineTakenToUpdate.VacMonthID;

                    HttpResponseMessage response = client.PutAsJsonAsync<VaccinesTaken>(urlUpdateVT, selectedVaccineTakenToUpdate).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        ShowMessages.ShowPopUp("Fejl. VaccineTaken blev ikke opdateret i databasen: " + response.StatusCode);
                    }
                    else
                    {
                        ShowMessages.ShowPopUp("VaccineTaken er nu opdateret i databasen");
                    }
                }

            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Fejl: " + e.Message);
            }
        }











        const string urlMonthToTakeVaccines = "api/MonthToTakeVaccines/";

        //GET MonthToTakeVaccines
        public static ObservableCollection<MonthToTakeVaccine> LoadMonthToTakeVaccineFromJsonAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.GetAsync(urlMonthToTakeVaccines).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var monthToTakeVaccinesList = response.Content.ReadAsAsync<ObservableCollection<MonthToTakeVaccine>>().Result;
                        return monthToTakeVaccinesList;
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Fejl: " + e.Message);

            }
            return null; // If error, return null
        }











        const string urlDoctors = "api/Doctors/";

        //GET Doctors
        public static ObservableCollection<Doctors> LoadDoctorsFromJsonAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serverUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = client.GetAsync(urlDoctors).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var doctorsList = response.Content.ReadAsAsync<ObservableCollection<Doctors>>().Result;
                        return doctorsList;
                    }
                }
            }
            catch (Exception e)
            {
                ShowMessages.ShowPopUp("Fejl: " + e.Message);

            }
            return null; // If error, return null
        }














        // Constructor
        public PersistencyService()
        {
            ShowMessage = new ShowMessages();

        }
    }
}
