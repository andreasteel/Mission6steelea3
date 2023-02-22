﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission6steelea3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6steelea3.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext _movieContext { get; set; }

        //constructor
        public HomeController(MovieContext something)
        {
            _movieContext = something;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MovieForm ()
        {
            return View("movieForm");
        }

        [HttpPost]
        public IActionResult MovieForm (MovieFormResponse mfr)
        {
            _movieContext.Add(mfr);
            _movieContext.SaveChanges();
            return View("Confirmation", mfr);
        }

        [HttpGet]
        public IActionResult Forms()
        {
            var applications = _movieContext.Responses
                .OrderBy(x => x.Title)
                .ToList();

            return View(applications);
        }

        public IActionResult Podcast()
        {
            return View("Podcast");
        }
     
    }
}
