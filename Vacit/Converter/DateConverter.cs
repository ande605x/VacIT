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
            return date.Day.ToString() + ". " + monthsDanish[date.Month + 1] + " " + date.Year; 
        }

        public static string AgeToStringDanish(DateTimeOffset birthdate)
        {
            var age = DateTimeOffset.Now - birthdate;

            if (age.Days < 32) return age.Days + " dage";
            else if (age.Days < 366) return age.Days/12 + " måneder "+ age.Days % 12 + " dage";
            else return age.Days/365 + " år " + (age.Days % 365)/12 + " måneder " + (age.Days % 365) % 12 + " dage";
        }
    }
}
