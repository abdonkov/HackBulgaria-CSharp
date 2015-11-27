using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace ClockAngle
{
    class Program
    {
        static double GetClockHandsAngle(DateTime time, bool exactHour = true)
        {
            if (exactHour)
            {
                double hourDegrees = time.Hour * 30;
                double minuteDegrees = time.Minute * 6;

                double resultAngle = Math.Abs(hourDegrees - minuteDegrees);

                return resultAngle < 180 ? resultAngle : 360 - resultAngle;
            }
            else
            {
                double hourDegrees = (60 * time.Hour + time.Minute) * 0.5;
                double minuteDegrees = time.Minute * 6;

                double resultAngle = Math.Abs(hourDegrees - minuteDegrees);

                return resultAngle < 180 ? resultAngle : 360 - resultAngle;
            }
        }
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            DateTime dt = new DateTime(2015, 1, 1, hour: 12, minute: 30, second: 0);

            Console.WriteLine("With exact hour calculation: {0:F2}°", GetClockHandsAngle(dt));
            Console.WriteLine("With non-exact hour calculation: {0:F2}°", GetClockHandsAngle(dt, false));
            Console.ReadKey();
        }
    }
}
