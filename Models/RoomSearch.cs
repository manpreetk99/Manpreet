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
        //Meaning the number of beds in the room; can only be 1, 2, or 3.
        [RegularExpression(@"[123]")]
        [Display(Name = "Number of beds")]
        public int BedCount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check-in date")]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check-out date")]
        public DateTime CheckOut { get; set; }


    }
}
