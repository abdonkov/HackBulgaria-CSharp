using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    class Time
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _day;
        private readonly int _hour;
        private readonly int _minute;
        private readonly int _second;
        public int Year { get { return _year; } }
        public int Month { get { return _month; } }
        public int Day { get { return _day; } }
        public int Hour { get { return _hour; } }
        public int Minute { get { return _minute; } }
        public int Second { get { return _second; } }

        public Time(int year, int month, int day, int hour, int minute, int second)
        {
            try
            {
                DateTime dt = new DateTime(year, month, day, hour, minute, second);
            }
            catch
            {
                throw new ArgumentException("Invalid date or time!");
            }
            _year = year;
            _month = month;
            _day = day;
            _hour = hour;
            _minute = minute;
            _second = second;
        }

        public Time(DateTime dt)
        {
            _year = dt.Year;
            _month = dt.Month;
            _day = dt.Day;
            _hour = dt.Hour;
            _minute = dt.Minute;
            _second = dt.Second;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2} {3}.{4}.{5}", Hour, Minute, Second, Day, Month, Year);
        }

        public static Time Now()
        {
            return new Time(DateTime.Now);
        }
    }
}
