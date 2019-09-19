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
        public async Task<IActionResult> Bookings(string sortOrder)
        {
            if (String.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "check-asc";
            }
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
            switch (sortOrder)
            {
                case "check-asc":
                    bookings = bookings.OrderBy(b => b.CheckIn);
                    break;
                case "check-desc":
                    bookings =bookings.OrderByDescending(b => b.CheckIn);
                    break;
                case "price-asc":
                    bookings = bookings.OrderBy(b => b.Cost);
                    break;
                case "price-desc":
                    bookings = bookings.OrderByDescending(b => b.Cost);
                    break;
                default:
                    bookings = bookings.OrderBy(b => b.CheckIn);
                    break;
            }
            ViewData["nextCheck"] = sortOrder != "check-asc" ? "check-asc" : "check-desc";
            ViewData["nextPrice"] = sortOrder != "price-asc" ? "price-asc" : "price-desc";
            return View(bookings);
        }
    }
}