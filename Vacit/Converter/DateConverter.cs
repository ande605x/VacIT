using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacit.Converter
{
    class DateConverter
    {
        public static DateTime DateTimeOffset_SetToDateTime(DateTimeOffset date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }
}
