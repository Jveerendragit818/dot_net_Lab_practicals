using experiment7_Dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace experiment7_Dotnet.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }
        // POST: Feedback
        [HttpPost]
        public ActionResult Index(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                return View("Result", feedback);
            }

            return View(feedback);
        }

    }
}