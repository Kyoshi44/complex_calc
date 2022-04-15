﻿using System;

public class ComplexNumber
{
    private double Real;
    private double Imaginary;
    

    public ComplexNumber(double real = 0.0, double imaginary = 0.0)
    {
        Real = real;
        Imaginary = imaginary;

    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    }
    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(((a.Real * b.Real + a.Imaginary * b.Imaginary) + (a.Imaginary * b.Real - a.Real * b.Imaginary)) / Math.Pow(b.Real, 2) + Math.Pow(b.Imaginary, 2));
    }
    public static bool operator ==(ComplexNumber a, ComplexNumber b)
    {
        return (a.Real == b.Real) && (a.Imaginary == b.Imaginary);
    }
    public static bool operator !=(ComplexNumber a, ComplexNumber b)
    {
        return !(a == b);
    }
    public static ComplexNumber operator -(ComplexNumber a)
    {
        return new ComplexNumber(-a.Real, -a.Imaginary);
    }



    public string Conversion()
    {
        double r = Math.Round(Math.Abs(Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2))),3);

        double fi = Math.Round(Math.Atan2(Imaginary, Real) / Math.PI ,3);

        return string.Format("{0} * e^({1}* \u03C0 * i)", r, fi); ;
    }

    public double Argument()
    {
        return Math.Atan2(Imaginary, Real);
    }

    public override string ToString()
    {
        if (Imaginary >= 0)
        {
            return string.Format("{0}+{1}i", Real, Imaginary);
        }
        return string.Format("{0}-{1}i", Real, -Imaginary);
    }
}