using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Bookings()
        {
            var bookings = _context.Booking.ToList();
            return View(bookings);
        }
    }
}