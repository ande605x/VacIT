using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacit.Model
{
    public class CardList
    {
        public string vaccineName { get; set; }
        public int monthToTake { get; set; }
        public bool taken { get; set; }

        public CardList()
        {

        }
        
    }



    //public IQueryable<CardList> GetCardList(int childID)
    //{
    //    // LINQ combinding 3 lists
    //    var cardList = from vt in VaccinesListSingleton.Instance.VaccinesTakenList
    //                   join vwm in VaccinesListSingleton.Instance.MonthToTakeVaccinesList
    //                   on vt.VacMonthID equals vwm.VacMonthID
    //                   join v in VaccinesListSingleton.Instance.VaccinesList
    //                   on vwm.VacID equals v.VacID
    //                   where vt.ChildID == childID
    //                   orderby vwm.MonthToTake
    //                   select new CardList() { vaccineName = v.VacName, monthToTake = vwm.MonthToTake, taken = vt.VacTaken };

    //    return cardList;

    //}


}
