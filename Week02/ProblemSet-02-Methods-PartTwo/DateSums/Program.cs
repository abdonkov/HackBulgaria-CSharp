using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DateSums
{
    class Program
    {
        static void PrintDatesWithGivenSum(int year, int sum)
        {
            DateTime dt = new DateTime(year, 1, 1);
            int yearSum = 0;
            int yearCopy = year;
            while (yearCopy != 0)
            {
                yearSum += yearCopy % 10;
                yearCopy /= 10;
            }

            while (dt.Year == year)
            {
                if (sum == dt.Day / 10 + dt.Day % 10 + dt.Month / 10 + dt.Month % 10 + yearSum)
                {
                    Console.WriteLine(dt.ToString("dd/MM/yyyy"));
                }
                dt = dt.AddDays(1);
            }
        }

        static void Main(string[] args)
        {
            int year = int.Parse(Console.ReadLine());
            int sum = int.Parse(Console.ReadLine());
            PrintDatesWithGivenSum(year, sum);
            Console.ReadKey();
        }
    }
}
