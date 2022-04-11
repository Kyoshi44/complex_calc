using Microsoft.AspNetCore.Mvc;
using Complex_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Complex_Calculator.Controllers
{
    public class CalcController : Controller
    {
        public IActionResult Calc()
        {
            return View();
        }

        public ActionResult Plus
        {

        }
    }
}
