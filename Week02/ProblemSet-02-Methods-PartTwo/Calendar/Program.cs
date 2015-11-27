using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace Calendar
{
    class Program
    {
        static void PrintCalendar(int month, int year, CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            DateTime dt = new DateTime(year, month, 1);
            string monthName = dt.ToString("MMMM");

            Console.WriteLine(char.ToUpper(monthName[0]) + monthName.Substring(1));
            string[,] calendarDates = new string[6, 7];

            int currentRow = 0;
            int currentCow = 0;
            int firstDayofWeek = 0;
            int dayChange = 0;
            int lastRow = 0;

            if (culture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday) dayChange = 1;

            while(dt.Month == month)
            {
                currentCow = (int)dt.DayOfWeek - dayChange;
                if (currentCow < 0) currentCow = 6;
                calendarDates[currentRow, currentCow] = dt.Day.ToString();
                lastRow = currentRow;
                if (currentCow == 0) firstDayofWeek = dt.Day;
                if (currentCow == 6) currentRow++;
                dt = dt.AddDays(1);
            }

            string[] daysOfWeek = new string[7];

            firstDayofWeek -= 7;
            dt = new DateTime(year, month, firstDayofWeek);

            for (int i = 0; i < 7; i++)
            {
                daysOfWeek[i] = dt.ToString("dddd");
                Console.Write(daysOfWeek[i] + " ");
                dt = dt.AddDays(1);
            }
            Console.WriteLine();

            for (int i = 0; i <= lastRow; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write("{0," + daysOfWeek[j].Length + "}", calendarDates[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            PrintCalendar(11, 2015, new CultureInfo("bg-BG"));
            Console.ReadLine();
        }
    }
}
