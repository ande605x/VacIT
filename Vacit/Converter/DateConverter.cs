using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vacit.Converter
{
    public class DateConverter
    {
        static string[] monthsDanish = {"januar","februar","marts","april","maj","juni","juli","august","september","oktober","november","december"};
        
        public static DateTime DateTimeOffset_SetToDateTime(DateTimeOffset date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public static string DateStringDanish(DateTimeOffset date)
        {
            return date.Day.ToString() + ". " + monthsDanish[date.Month - 1] + " " + date.Year; 
        }

        public static string MonthsToYears(int months)
        {
            if (months == 1) return "1 måned";
            else if (months < 12) return months + " måneder";
            else if (months % 12 == 0) return months / 12 + " år";
            else if (months % 12 == 1) return months / 12 + " år 1 måned";            
            else return months / 12 + " år " + months % 12 + " måneder";
        }

        public static string AgeToStringDanish(DateTimeOffset birthdate)
        {
            var age = DateTimeOffset.Now - birthdate;

            if (age.Days < 0) age = -age; // For WhenToTakeStringDanish in VaccineCards calculation

            if (age.Days < 14)
            {
                if (age.Days == 1) return "1 dag";
                else return age.Days + " dage";
            }
            else if (age.Days<60)                                                             // if lees than 2 months
            {
                return age.Days / 7 + " uger"; //Rounded up to not display days
            }
            else if (age.Days < 366-30)                                                       // if less than 11 months
            {               
                return age.Days / 30 + " måneder";                                            
            }
            else
            {
                if ((age.Days % 365) == 0) return age.Days / 365 + " år";                     // if excatly whole years
                else if (age.Days % 365 > 365 - 30) return (age.Days / 365) + 1 + " år";      // if years + 11-12 months: rounded up to whole years
                else
                {
                    if ((age.Days % 365) / 30 == 0)                                           // prevent XX years 0 months 
                        return age.Days / 365 + " år";
                    else 
                        return age.Days / 365 + " år " + (age.Days % 365) / 30 + " måneder"; 
                }
            }
        }
    }
}
