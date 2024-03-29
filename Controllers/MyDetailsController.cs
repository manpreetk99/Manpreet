﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BadgerysCreekHotel.Data;
using BadgerysCreekHotel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BadgerysCreekHotel.Controllers
{
    [Authorize(Roles = "Customer")]
    public class MyDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            string _email = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _context.Customer.FindAsync(_email);

            if (user == null)
            {
                user = new Models.Customer { Email = _email };
                return View("~/Views/MyDetails/Create.cshtml", user);
            }
            else
            {
                return View("~/Views/MyDetails/Update.cshtml", user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetails([Bind("Email, GivenName, Surname, PostCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return SuccessPageReturn(customer);
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDetails([Bind("Email, GivenName, Surname, PostCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return SuccessPageReturn(customer);
            }
            return View(customer);
        }

        //Displays a success page for 3 seconds and then redirect to the home page.
        private IActionResult SuccessPageReturn(Customer customer)
        {
            Response.Headers.Add("REFRESH", "3; URL = /");
            return View("~/Views/MyDetails/Success.cshtml", customer);
        }
    }
}