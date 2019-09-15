using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerysCreekHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult SearchRooms([Bind("BedCount", "CheckIn", "CheckOut")] RoomSearch searchParameters)//
        {
            if (ModelState.IsValid)
            {
                return View("/");
            }


            return View("/");
        }
    }
}