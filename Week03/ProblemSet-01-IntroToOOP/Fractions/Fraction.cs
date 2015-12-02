using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractions
{
    class Fraction
    {
        private int numerator;
        private int denominator;

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0) throw new ArgumentException("Denominator cannot be 0!");
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public int Numerator
        {
            get { return numerator; }
            set { denominator = value; }
        }

        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0) throw new ArgumentException("Denominator cannot be 0!");

                denominator = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", numerator, denominator);
        }

        public override bool Equals(object obj)
        {
            if (obj is Fraction)
            {
                Fraction frac = obj as Fraction;
                if (numerator != frac.Numerator) return false;
                if (denominator != frac.Denominator) return false;

                return true;
            }
            else return false;
        }

        public static bool operator ==(Fraction frac1, Fraction frac2)
        {
            return frac1.Equals(frac2);
        }

        public static bool operator !=(Fraction frac1, Fraction frac2)
        {
            return !frac1.Equals(frac2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + numerator.GetHashCode();
                hash = hash * 23 + denominator.GetHashCode();
                return hash;
            }
        }

        private int GreatestCommonDenominator(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            int temp;
            while (b != 0)
            {
                temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }

        public Fraction Simplify()
        {
            int gcd = GreatestCommonDenominator(numerator, denominator);
            numerator /= gcd;
            denominator /= gcd;

            if (denominator < 0)
            {
                denominator *= -1;
                numerator *= -1;
            }
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator +(Fraction frac1, Fraction frac2)
        {
            int numerator;
            int denominator;
            if (frac1.denominator == frac2.denominator)
            {
                numerator = frac1.numerator + frac2.numerator;
                denominator = frac1.denominator;
            }
            else
            {
                numerator = frac1.numerator * frac2.denominator + frac2.numerator * frac1.denominator;
                denominator = frac1.denominator * frac2.denominator;
            }
            return new Fraction(numerator, denominator).Simplify();
        }

        public static Fraction operator -(Fraction frac1, Fraction frac2)
        {
            int numerator;
            int denominator;
            if (frac1.denominator == frac2.denominator)
            {
                numerator = frac1.numerator - frac2.numerator;
                denominator = frac1.denominator;
            }
            else
            {
                numerator = frac1.numerator * frac2.denominator - frac2.numerator * frac1.denominator;
                denominator = frac1.denominator * frac2.denominator;
            }
            return new Fraction(numerator, denominator).Simplify();
        }

        public static Fraction operator *(Fraction frac1, Fraction frac2)
        {
            int numerator = frac1.numerator * frac2.numerator;
            int denominator = frac2.denominator * frac2.denominator;
            return new Fraction(numerator, denominator).Simplify();
        }

        public static Fraction operator /(Fraction frac1, Fraction frac2)
        {
            int numerator = frac1.numerator * frac2.denominator;
            int denominator = frac2.denominator * frac2.numerator;
            return new Fraction(numerator, denominator).Simplify();
        }

        public static double operator +(Fraction frac, double number)
        {
            //double numerator = frac.numerator + frac.denominator * number;
            //double denominator = (double)frac.denominator;
            //return numerator / denominator;
            return (double)frac + number;
        }

        public static double operator +(double number, Fraction frac)
        {
            //double numerator = frac.numerator + frac.denominator * number;
            //double denominator = (double)frac.denominator;
            //return numerator / denominator;
            return number + (double)frac;
        }

        public static double operator -(Fraction frac, double number)
        {
            //double numerator = frac.numerator - frac.denominator * number;
            //double denominator = (double)frac.denominator;
            //return numerator / denominator;
            return (double)frac - number;
        }

        public static double operator -(double number, Fraction frac)
        {
            //double numerator = frac.denominator * number - frac.numerator;
            //double denominator = (double)frac.denominator;
            //return numerator / denominator;
            return number - (double)frac;
        }

        public static double operator *(Fraction frac, double number)
        {
            //double numerator = frac.numerator * number;
            //double denominator = (double)frac.denominator;
            //return numerator / denominator;
            return (double)frac * number;
        }

        public static double operator *(double number, Fraction frac)
        {
            //double numerator = frac.numerator * number;
            //double denominator = (double)frac.denominator;
            //return numerator / denominator;
            return number * (double)frac;
        }

        public static double operator /(Fraction frac, double number)
        {
            //double numerator = frac.numerator / number;
            //double denominator = (double)frac.denominator;
            //return numerator / denominator;
            return (double)frac / number;
        }

        public static double operator /(double number, Fraction frac)
        {
            //double numerator = frac.denominator * number;
            //double denominator = (double)frac.numerator;
            //return numerator / denominator;
            return number / (double)frac;
        }
        
        public static explicit operator double(Fraction frac)
        {
            return (double)frac.numerator / (double)frac.denominator;
        }
    }
}
