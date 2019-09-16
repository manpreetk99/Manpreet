using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerysCreekHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace BadgerysCreekHotel.Controllers
{
    [Authorize(Roles = "Customer")]
    public class SearchRoomsController : Controller
    {

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

        public IActionResult SearchRooms([Bind("BedCount", "CheckIn", "CheckOut")] RoomSearch searchParameters)//
        {
            string query = "SELECT ID, 'Level', BedCount, Price FROM Room WHERE NOT EXISTS ( SELECT * FROM Booking WHERE mydate >= CheckIn and mydate <= CheckOut)";
            var occupationStart = new SqliteParameter("mydate", searchParameters.CheckIn);
            var occupationEnd = new SqliteParameter("mydate", searchParameters.CheckOut);
            if (ModelState.IsValid)
            {
                return View("/");
            }


            return View("/");
        }
    }
}