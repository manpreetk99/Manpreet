using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.ViewModels
{
    public class StatView
    {
        public List<CustomerStat> CustomerStats { get; set; }
        public List<RoomStat> RoomStats { get; set; }
    }

    public class CustomerStat
    {
        public int Count { get; set; }
        public string PostCode { get; set; }
    }

    public class RoomStat
    {
        public int Count { get; set; }
        public int RoomID { get; set; }
    }
}
