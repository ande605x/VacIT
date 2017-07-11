using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacit.Model
{
    public class VaccinesCard
    {

        //public string ChildI { get; set; }

        //public string AgeIconFilename { get; set; }

        //public int CardColor { get; set; }

        //public int VaccineID { get; set; }

        //public int VacMonthID { get; set; }



        public string VaccineName { get; set; }
        public int MonthToTake { get; set; }
        public bool Taken { get; set; }


        public string MonthAndYearsToTakeStringDanish { get; set; }

        public string CardColor { get; set; }

        public string AgeIconFilename { get; set; }

        public double AgeIconOpacity { get; set; }


        public VaccinesCard(string vaccineName, int monthToTake, bool taken, bool genderGirl)
        {

            this.MonthToTake = monthToTake;
            this.VaccineName = vaccineName;
            this.Taken = taken;

            this.MonthAndYearsToTakeStringDanish = Converter.DateConverter.MonthsToYears(monthToTake);



            if (taken) CardColor = "#7F444444"; // tidligere #66444444 flottere
            else if (genderGirl) CardColor = "#CCB94CA6"; else CardColor = "#CC006C95";     //could also write GenderColor="Blue"

            AgeIconFilename = "ms-appx:///Assets/";
            if (genderGirl)
            {
                if (monthToTake < 4) AgeIconFilename += "Girl1.png";
                else if (monthToTake < 12) AgeIconFilename += "Girl2.png";
                else if (monthToTake < 15) AgeIconFilename += "Girl3.png";
                else if (monthToTake < 48) AgeIconFilename += "Girl4.png";
                else if (monthToTake < 60) AgeIconFilename += "Girl5.png";
                else if (monthToTake < 144) AgeIconFilename += "Girl6.png";
                else AgeIconFilename += "Girl7.png";
            }
            else
            {
                if (monthToTake < 4) AgeIconFilename += "Boy1.png";
                else if (monthToTake < 12) AgeIconFilename += "Boy2.png";
                else if (monthToTake < 15) AgeIconFilename += "Boy3.png";
                else if (monthToTake < 48) AgeIconFilename += "Boy4.png";
                else if (monthToTake < 60) AgeIconFilename += "Boy5.png";
                else if (monthToTake < 144) AgeIconFilename += "Boy6.png";
                else AgeIconFilename += "Boy7.png";
            }

            if (taken) AgeIconOpacity = 0.5; else AgeIconOpacity = 1.0;

        }
        }
    }
