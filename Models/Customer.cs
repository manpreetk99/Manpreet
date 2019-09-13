using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.Models
{
    public class Customer
    {
        [Key, Required]
        [DataType(DataType.EmailAddress)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="E-mail Address")]
        public string Email { get; set; }

        public string Surname { get; set; }
        [Display(Name ="Given Name")]
        public string GivenName { get; set; }

        [Display(Name ="Post Code")]
        public string PostCode { get; set; }

        //Assuming this does not require a display name attribute.
        public ICollection<Booking> TheBookings { get; set; }
    }
}
