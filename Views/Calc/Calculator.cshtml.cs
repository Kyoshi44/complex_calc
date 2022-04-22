using System;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
            if (new Regex(@"[^\dij+\-]").IsMatch(data) || data.Equals(""))
            {
                
                return new ComplexNumber(0,0);
            }

            Regex complex = new Regex(@"(?<real>(?:[+-])?(?:\d+)(?:[,.](?:\d+))?(?![ij]))?(?<imaginary>(?:[+-])?(?:\d+)(?:[,.](?:\d+))?[ij])?");
            
            string real = complex.Match(data).Groups["real"].ToString();
            if (real == "")
            {
                real = "0";
            }

            string img = complex.Match(data).Groups["imaginary"].ToString();
            if (img == "")
            {
                img = "0";
            }
            real = real.TrimEnd('+', '-');
            img = img.TrimEnd('i', 'j');

            return new ComplexNumber(Convert.ToDouble(real), Convert.ToDouble(img));
        }
        public ComplexNumber[] GetComplexNumbers()
        {
            Regex complex = new Regex(@"[^\dij+\-]");
            string number1 = HttpContext.Request.Form["Number1"];
            string number2 = HttpContext.Request.Form["Number2"];
            
            ComplexNumber num1 = CreateComplexNumber(number1);
            ComplexNumber num2 = CreateComplexNumber(number2);

            return new[] {num1, num2};
        }

        public IActionResult Add()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            try
            {
                ViewBag.output = (complexNumbers[0] + complexNumbers[1]).ToString();
                ViewBag.number1 = complexNumbers[0].ToString();
                ViewBag.number2 = complexNumbers[1].ToString();
            }
            catch (NullReferenceException e)
            {
                ViewBag.output = "Please input number"+ e.Message;
            }

            return View("Calculator");
        }

        public IActionResult Substract()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            try
            {
                ViewBag.output = (complexNumbers[0] - complexNumbers[1]).ToString();
                ViewBag.number1 = complexNumbers[0].ToString();
                ViewBag.number2 = complexNumbers[1].ToString();
            }
            catch (Exception e)
            {
                ViewBag.output = "Please input number";
            }

            return View("Calculator");
        }
        
        public IActionResult Multiply()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            try
            {
                ViewBag.output = (complexNumbers[0] * complexNumbers[1]).ToString();
                ViewBag.number1 = complexNumbers[0].ToString();
                ViewBag.number2 = complexNumbers[1].ToString();
            }
            catch (Exception e)
            {
                ViewBag.output = "Please input number";
            }
            
            return View("Calculator");
        }
        public IActionResult Divide()
        {
            ComplexNumber[] complexNumbers = GetComplexNumbers();
            try
            {
                ViewBag.output = (complexNumbers[0] / complexNumbers[1]).ToString();
                ViewBag.number1 = complexNumbers[0].ToString();
                ViewBag.number2 = complexNumbers[1].ToString();
            }
            catch (Exception e)
            {
                ViewBag.output = "" + e.Message;
            }

            return View("Calculator");
            
        }

        public IActionResult ExpConversion()
        {
            StringBuilder sb = new StringBuilder();
            Regex complex = new Regex(@"[^\dij+\-]");
            string number1 = HttpContext.Request.Form["Number1"];
            string number2 = HttpContext.Request.Form["Number2"];

            if (complex.IsMatch(number1) || complex.IsMatch(number2) || number1.Equals("") && number2.Equals(""))
            {
                ViewBag.output = "Please input number";
                return View("Calculator");
            }

            if (number1.Length != 0)
            {
                sb.AppendLine(CreateComplexNumber(number1).Conversion());
                ViewBag.output = sb;
            }

            if (number2.Length != 0)
            {
                sb.AppendLine(CreateComplexNumber(number2).Conversion());
                ViewBag.output = sb;
            }
            return View("Calculator");
        }
    }
}
