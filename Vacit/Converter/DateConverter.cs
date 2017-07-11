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



            //if (age.Days < 31)
            //{
            //    if (age.Days == 1) return "1 dag";
            //    else return age.Days + " dage";
            //}
            //else if (age.Days < 366) //Rounded up to not display days
            //{
            //    if (age.Days/30==1) return "1 måned "+age.Days%30+" dage";
            //    return age.Days / 30 + " måneder";
            //    //if (age.Days%30==0) return age.Days / 30 + " måneder";
            //    //else return age.Days / 30 + " måneder " + age.Days % 30 + " dage";
            //}

            if (age.Days < 14)
            {
                if (age.Days == 1) return "1 dag";
                else return age.Days + " dage";
            }
            else if (age.Days<60)
            {
                return age.Days / 7 + " uger";
            }
            else if (age.Days < 366) //Rounded up to not display days
            {
                
                return age.Days / 30 + " måneder";
                //if (age.Days%30==0) return age.Days / 30 + " måneder";
                //else return age.Days / 30 + " måneder " + age.Days % 30 + " dage";
            }
            else
            {
                if ((age.Days % 365) == 0) return age.Days / 365 + " år";
                else return age.Days / 365 + " år " + (age.Days % 365) / 30 + " måneder";
                //    if ((age.Days % 365) % 30 == 0) return age.Days / 365 + " år " + (age.Days % 365) / 30 + " måneder";
                //    else return age.Days / 365 + " år " + (age.Days % 365) / 30 + " måneder " + (age.Days % 365) % 30 + " dage";
                //
            }
        }
    }
}
