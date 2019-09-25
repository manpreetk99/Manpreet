using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerysCreekHotel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadgerysCreekHotel.ViewModels;
namespace BadgerysCreekHotel.Controllers
{

    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Statistics()
        {
            var customers =
                from customer in _context.Customer
                group customer by customer.PostCode into g
                select new CustomerStat
                {
                    PostCode = g.Key,
                    Count = g.Count()
                };

            var rooms =
                from room in _context.Booking
                group room by room.RoomID into g
                select new RoomStat
                {
                    RoomID = g.Key,
                    Count = g.Count()
                };
            var customerStats = await customers.ToListAsync();
            var roomStats = await rooms.ToListAsync();

            StatView statView = new StatView
            {
                CustomerStats = customerStats,
                RoomStats = roomStats
            };

            return View(statView);
        }
    }
}