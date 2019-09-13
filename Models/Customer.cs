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


        /* Required; Length ranges between 2 and 20 
         * characters inclusive; Can only consist 
         * of English letters, hyphen and apostrophe */
        [RegularExpression(@"[a-zA-Z- ']")]
        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string Surname { get; set; }

        /* Required; Length ranges between 2 and 20 
        * characters inclusive; Can only consist 
        * of English letters, hyphen and apostrophe */
        [RegularExpression(@"[a-zA-Z- ']")]
        [StringLength(20, MinimumLength = 2)]
        [Required]
        [Display(Name ="Given Name")]
        public string GivenName { get; set; }

        //Exactly 4 digits
        [Required]
        [StringLength(4, MinimumLength = 4)]
        [DataType(DataType.PostalCode)]
        [Display(Name ="Post Code")]
        public string PostCode { get; set; }

        //Assuming this does not require a display name attribute.
        public ICollection<Booking> TheBookings { get; set; }
    }
}
