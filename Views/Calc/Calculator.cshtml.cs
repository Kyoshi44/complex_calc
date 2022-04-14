using System;
using Microsoft.AspNetCore.Mvc;

namespace Complex_Calculator.Views.Calc
{
    public class Calc : Controller
    {
        public IActionResult Calculator()
        {
            return View();
        }

        private ComplexNumber GetComplexNumber(String data)
        {
            String[] input = data.Replace("i", "").Split("+");
            return new ComplexNumber(Convert.ToDouble(input[0]), Convert.ToDouble(input[1]));

        }
        public ComplexNumber[] GetComplexNumbers()
        {
            string number1 = HttpContext.Request.Form["Number1"];
            string number2 = HttpContext.Request.Form["Number2"];
            return new []{GetComplexNumber(number1), GetComplexNumber(number2)};
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


        public IActionResult Conversion()
        {
            string number1 = HttpContext.Request.Form["Number1"];
            if (number1.Length != 0)
            {
                ViewBag.output = GetComplexNumber(number1).Conversion();
            }
            string number2 = HttpContext.Request.Form["Number2"];
            if (number2.Length != 0)
            {
                ViewBag.output = GetComplexNumber(number2).Conversion();
            }
            return View("Calculator");
        }
    }
}
