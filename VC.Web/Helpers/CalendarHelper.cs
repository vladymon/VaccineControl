using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VC.Web.Helpers
{
    public class CalendarHelper : ICalendarHelper
    {
        public int GetPosition(string week)
        {
            var position = 0;
            switch(week)
            {
                case "domingo":
                    position = 1;
                    break;
                case "lunes":
                    position = 2;
                    break;
                case "martes":
                    position = 3;
                    break;
                case "miércoles":
                    position = 4;
                    break;
                case "jueves":
                    position = 5;
                    break;
                case "viernes":
                    position = 6;
                    break;
                case "sábado":
                    position = 7;
                    break;
                default:
                    position = 0;
                    break;
            }
            return position;
        }
    }
}
