using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridayThe13th
{
    class Program
    {
        static int UnfortunateFridays(int startYear, int endYear)
        {
            int unfortCounter = 0;
            if (startYear > endYear)
            {
                int temp = startYear;
                startYear = endYear;
                endYear = temp;
            }
            DateTime dt = new DateTime(startYear, 1, 13);
            while(dt.Year <= endYear)
            {
                if (dt.DayOfWeek == DayOfWeek.Friday) unfortCounter++;
                dt = dt.AddMonths(1);
            }
            return unfortCounter;
        }

        static void Main(string[] args)
        {
            int startYear = int.Parse(Console.ReadLine());
            int endYear = int.Parse(Console.ReadLine());
            Console.WriteLine(UnfortunateFridays(startYear,endYear));
            Console.ReadKey();
        }
    }
}
