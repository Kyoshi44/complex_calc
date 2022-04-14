using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace Complex_Calculator.Views.Calc
{
    public class Calc : Controller
    {
        public IActionResult Calculator()
        {
            return View();
        }

        private ComplexNumber CreateComplexNumber(String data)
        {
            Regex real = new Regex(@"[+-]?\d[^ij]");
            Regex imaginary = new Regex(@"[+-]?\d[ij]");
            
            string realPart = real.Match(data).ToString();
            string imaginaryPart = imaginary.Match(data).ToString();

            imaginaryPart = imaginaryPart.TrimEnd('i', 'j');
            realPart = realPart.TrimEnd('+', '-');

            return new ComplexNumber(Convert.ToDouble(realPart), Convert.ToDouble(imaginaryPart));

        }
        public ComplexNumber[] GetComplexNumbers()
        {
            string number1 = HttpContext.Request.Form["Number1"];
            string number2 = HttpContext.Request.Form["Number2"];
            return new []{CreateComplexNumber(number1), CreateComplexNumber(number2)};
        }

        public IActionResult Add()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            ViewBag.output = (complexNumbers[0] + complexNumbers[1]).ToString();
            ViewBag.number1 = complexNumbers[0].ToString();
            ViewBag.number2 = complexNumbers[1].ToString();
            return View("Calculator");
        }

        public IActionResult Substract()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            ViewBag.output = (complexNumbers[0] - complexNumbers[1]).ToString();
            ViewBag.number1 = complexNumbers[0].ToString();
            ViewBag.number2 = complexNumbers[1].ToString();
            return View("Calculator");
        }
        public IActionResult Multiply()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            ViewBag.output = (complexNumbers[0] * complexNumbers[1]).ToString();
            ViewBag.number1 = complexNumbers[0].ToString();
            ViewBag.number2 = complexNumbers[1].ToString();
            return View("Calculator");
        }
        public IActionResult Divide()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            ViewBag.output = (complexNumbers[0] / complexNumbers[1]).ToString();
            ViewBag.number1 = complexNumbers[0].ToString();
            ViewBag.number2 = complexNumbers[1].ToString();
            return View("Calculator");
        }


        public IActionResult ExpConversion()
        {
            string number1 = HttpContext.Request.Form["Number1"];
            if (number1.Length != 0)
            {
                ViewBag.output = CreateComplexNumber(number1).Conversion();
            }
            string number2 = HttpContext.Request.Form["Number2"];
            if (number2.Length != 0)
            {
                ViewBag.output = CreateComplexNumber(number2).Conversion();
            }
            return View("Calculator");
        }
    }
}
