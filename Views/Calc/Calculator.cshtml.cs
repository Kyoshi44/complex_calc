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
            if ((new Regex(@"[^\dij+\-]").IsMatch(data)) || data.Equals("")) return null;
            Regex realRegex = new Regex(@"[+-]?\d+[^ij]");
            Regex imaginaryRegex= new Regex(@"[+-]?\d+[ij]");

            string real = realRegex.Match(data).ToString();
            string imaginary = imaginaryRegex.Match(data).ToString();

            real = real.TrimEnd('+', '-');
            imaginary = imaginary.TrimEnd('i', 'j');

            return new ComplexNumber(Convert.ToDouble(real), Convert.ToDouble(imaginary));
        }
        public ComplexNumber[] GetComplexNumbers()
        {
            string number1 = HttpContext.Request.Form["Number1"];
            string number2 = HttpContext.Request.Form["Number2"];

            ComplexNumber num1 = CreateComplexNumber(number1);
            ComplexNumber num2 = CreateComplexNumber(number2);
            
            if (num1 is null || num2 is null) 
            {
                return null;
            }

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
                ViewBag.output = "Please input number";
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
                ViewBag.output = "Please input number";
            }

            return View("Calculator");
            
        }
        public IActionResult ExpConversion()
        {
            StringBuilder sb = new StringBuilder();
            string number1 = HttpContext.Request.Form["Number1"];
            if ((new Regex(@"[^\dij+\-]/^$/").IsMatch(number1)) || number1.Equals(""))
            {
                ViewBag.output = "Please input number";
                return View("Calculator");
            }
            if (number1.Length != 0)
            {
                sb.AppendLine(CreateComplexNumber(number1).Conversion());
            }
            string number2 = HttpContext.Request.Form["Number2"];
            if ((new Regex(@"[^\dij+\-]/^$/").IsMatch(number2)) || number2.Equals("")) return View("Calculator");
            if (number2.Length != 0)
            {
                sb.AppendLine(CreateComplexNumber(number2).Conversion());
            }
            ViewBag.output = sb;
            
            return View("Calculator");
        }
    }
}
