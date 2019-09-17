using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BadgerysCreekHotel.Data;
using BadgerysCreekHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadgerysCreekHotel.Controllers
{
    [Authorize(Roles = "Customer")]
    public class BookRoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookRoomController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MakeBooking([Bind("RoomID, CheckIn, CheckOut")] Booking booking)
        {
            //construct raw sql to see if we can make the booking
            string query = "SELECT Room.ID, Level, BedCount, Booking.CheckIn, Booking.CheckOut, Booking.CustomerEmail, Room.Price " +
                "FROM Room LEFT JOIN Booking ON Room.ID = Booking.RoomID " +
                "WHERE Room.ID = @ROOMID AND " +
                "Room.ID NOT IN(    " +
                     "SELECT Room.ID " +
                     "FROM Room " +
                     "LEFT JOIN Booking " +
                     "ON Room.ID = Booking.RoomID " +
                     "WHERE (date(@INCOMINGSTART) >= date(checkIn) and date(@INCOMINGSTART) <= date(checkOut)) or (date(@INCOMINGEND) <= date(checkOut) and date(@INCOMINGEND) >= date(CheckIn)))";
            var para1 = new SqliteParameter("INCOMINGSTART", booking.CheckIn);
            var para2 = new SqliteParameter("INCOMINGEND", booking.CheckOut);
            var para3 = new SqliteParameter("ROOMID", booking.RoomID);
            var queryResult = await _context.Room.FromSql(query, para1,para2,para3).FirstOrDefaultAsync();
            //if the booking is unavailable
            if (queryResult != null)// the booking can be done
            {
                //Work out the cost
                booking.Cost = queryResult.Price * DaysOfStay(booking.CheckIn, booking.CheckOut);
                //Work out the logged in customers email.
                booking.CustomerEmail = User.FindFirst(ClaimTypes.Name).Value;

                booking.TheRoom = queryResult; // aquire the room and store it here.
                await _context.AddAsync(booking); //
                await _context.SaveChangesAsync(); // Save the changes.

            }
            return View("~/Views/BookRoom/Index.cshtml", booking);
        }
        //compares two datetimes and returns the decimal number of days stayed.
        private decimal DaysOfStay(DateTime checkIn, DateTime checkOut)
        {
            return (decimal)(checkOut - checkIn).TotalDays;
        }

        private IActionResult Unavailable()
        {
            return View("~/Views/BookRoom/Unavailable.cshtml");
        }
    }
}