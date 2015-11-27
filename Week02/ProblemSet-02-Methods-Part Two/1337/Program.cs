using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1337
{
    class Program
    {
        static void HackerTime()
        {
            DateTime curDt = DateTime.Now;
            DateTime hackDt = new DateTime(DateTime.Now.Year, 12, 21, 13, 37, 0);
            if (curDt > hackDt) hackDt = hackDt.AddYears(1);
            TimeSpan tm = hackDt - curDt;
            Console.WriteLine("{0:dd\\:hh\\:mm}", tm);
        }

        static void Main(string[] args)
        {
            HackerTime();
            Console.ReadKey();
        }
    }
}
