using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BadgerysCreekHotel.Controllers
{
    public class SearchRoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}