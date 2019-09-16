using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.Models
{
    public class Room
    {
        public int ID { get; set; }

        //Meaning the level of this room; Exactly one character of ‘G’, ‘1’, ‘2’, or ‘3’.Required.
        [StringLength(1, MinimumLength = 1)]
        [Required]
        [RegularExpression(@"[G123]{1}")]
        public string Level { get; set; }

        //Meaning the number of beds in the room; can only be 1, 2, or 3.
        [RegularExpression(@"[123]{1}")]
        public int BedCount { get; set; }

        //Meaning the price per night; Between $50 and $300.
        [DataType(DataType.Currency)]
        [Range(50.0,300.0)]
        public decimal Price { get; set; }

        public ICollection<Booking> TheBookings { get; set; }


    }
}
