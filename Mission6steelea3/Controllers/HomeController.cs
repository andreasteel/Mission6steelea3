﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.cat = _movieContext.Categories.ToList();
            return View("movieForm");
        }

        [HttpPost]
        public IActionResult MovieForm (MovieFormResponse mfr)
        {
            if (ModelState.IsValid)
            {
                _movieContext.Add(mfr);
                _movieContext.SaveChanges();
                return View("Confirmation", mfr);
            }
            else
            {
                ViewBag.cat = _movieContext.Categories.ToList();
                return View(mfr);
            }
        }

        [HttpGet]
        public IActionResult Forms()
        {
            var applications = _movieContext.Responses
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(applications);
        }

        public IActionResult Podcast()
        {
            return View("Podcast");
        }


        [HttpGet]
        public IActionResult Edit (int id)
        {
            ViewBag.cat = _movieContext.Categories.ToList();

            var movie = _movieContext.Responses.Single(x => x.FormId == id);

            return View("movieForm", movie);
        }

        [HttpPost]
        public IActionResult Edit (MovieFormResponse blah)
        {
            _movieContext.Update(blah);
            _movieContext.SaveChanges();

            return RedirectToAction("Forms");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie =_movieContext.Responses.Single(x => x.FormId == id);
            return View("Delete", movie);
        }

        [HttpPost]
        public IActionResult Delete(MovieFormResponse mfr)
        {
            _movieContext.Responses.Remove(mfr);
            _movieContext.SaveChanges();

            return RedirectToAction("Forms");
        }

    }
}
