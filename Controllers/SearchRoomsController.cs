using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerysCreekHotel.Data;
using BadgerysCreekHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BadgerysCreekHotel.Controllers
{
    [Authorize(Roles = "Customer")]
    public class SearchRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Probably delete this
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            var newSearch = new Models.RoomSearch();
            return View("~/Views/SearchRooms/SearchRooms.cshtml", newSearch);
        }


        /* RAW SQL QUERY WORKING PROTOTYPE.
         SELECT Room.ID, Level, BedCount, Booking.CheckIn, Booking.CheckOut, Booking.CustomerEmail
            FROM Room 
            LEFT JOIN Booking
            ON Room.ID = Booking.RoomID
            WHERE Room.ID NOT IN ( 
	            SELECT Room.ID
	            FROM Room 
	            LEFT JOIN Booking
	            ON Room.ID = Booking.RoomID
	            WHERE (date('2019-09-12 00:00:00') >= date(checkIn) and date('2019-09-12 00:00:00') <= date(checkOut)) or (date('2019-09-15 00:00:00') <= date(checkOut) and date('2019-09-15 00:00:00') >= date(CheckIn))
                )
             */

        public async Task<IActionResult> SearchRooms([Bind("BedCount", "CheckIn", "CheckOut")] RoomSearch searchParameters)//
        {
            string query = "SELECT Room.ID, Level, BedCount, Booking.CheckIn, Booking.CheckOut, Booking.CustomerEmail, Room.Price " +
                "FROM Room LEFT JOIN Booking ON Room.ID = Booking.RoomID " +
                "WHERE Room.ID NOT IN(    " +
                    "SELECT Room.ID " +
                    "FROM Room " +
                    "LEFT JOIN Booking " +
                    "ON Room.ID = Booking.RoomID " +
                    "WHERE (date(@INCOMINGSTART) >= date(checkIn) and date(@INCOMINGSTART) <= date(checkOut)) or (date(@INCOMINGEND) <= date(checkOut) and date(@INCOMINGEND) >= date(CheckIn)))";
            var occupationStart = new SqliteParameter("INCOMINGSTART", searchParameters.CheckIn);
            var occupationEnd = new SqliteParameter("INCOMINGEND", searchParameters.CheckOut);
            var freeRooms = await _context.Room.FromSql(query, occupationStart, occupationEnd).ToListAsync();
            if (ModelState.IsValid)
            {
                return View("/");
            }


            return View("/");
        }
    }
}