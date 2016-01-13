using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticClassesAndMethodsLibrary
{
    public class Configuration
    {
        public char GetReplacingValue()
        {
            var currentTime = DateTime.Now;
            int digitSum = currentTime.Year / 1000
                + currentTime.Year / 100 % 10
                + currentTime.Year / 10 % 10
                + currentTime.Year % 10
                + currentTime.Month / 10
                + currentTime.Month % 10
                + currentTime.Day / 10
                + currentTime.Day % 10
                + currentTime.Hour / 10
                + currentTime.Hour % 10
                + currentTime.Minute / 10
                + currentTime.Minute % 10
                + currentTime.Second / 10
                + currentTime.Second % 10;

            return (char)(digitSum % 25 + 65);
        }
    }
}
