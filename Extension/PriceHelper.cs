using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.Extension
{
    public static class PriceHelper
    {
        public static decimal DaysOfStay(this DateTime checkIn, DateTime checkOut)
        {
            return (decimal)(checkOut - checkIn).TotalDays;
        }
    }
}
