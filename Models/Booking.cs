using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.Models
{
    public class Booking
    {
        //Primary key
        public int ID { get; set; }

        //Foreign Key
        public int RoomID { get; set; }

        [Display(Name = "E-mail address")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Display(Name = "Check-in date")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check-out date")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        public Room TheRoom { get; set; }

        public Customer TheCustomer { get; set; }
    }
}
