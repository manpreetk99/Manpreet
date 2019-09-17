using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BadgerysCreekHotel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BadgerysCreekHotel.Controllers
{
    [Authorize(Roles = "Customer")]
    public class MyBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MyBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Bookings()
        {
            string customer = User.FindFirst(ClaimTypes.Name).Value; // get logged in user name
            //var bookings = _context.Booking.Where(.ToList();
            var bookings =
                from booking in _context.Booking
                where booking.CustomerEmail == customer
                select booking;

            //ensure that the bookings associate with the customer
            foreach (var booking in bookings)
            {
                var user = await _context.Customer.FindAsync(customer);
                booking.TheCustomer = user;
            }
            return View(bookings);
        }
    }
}