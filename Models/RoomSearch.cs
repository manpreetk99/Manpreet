using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.Models
{
    //This model will be used to validate the search room and will not be scaffolded or migrated.
    public class RoomSearch
    {
        public List<SelectListItem> Beds;
        public List<Room> AvailableRooms { get; set; }
        public RoomSearch()
        {
            Beds = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "One bed"},
                new SelectListItem { Value = "2", Text = "Two beds"},
                new SelectListItem { Value = "3", Text = "Three beds"}
            };
            this.CheckIn = DateTime.Now;
            this.CheckOut = DateTime.Now;
        }
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
        [Range(50.0, 300.0)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Check-in date")]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Check-out date")]
        public DateTime CheckOut { get; set; }


    }
}
