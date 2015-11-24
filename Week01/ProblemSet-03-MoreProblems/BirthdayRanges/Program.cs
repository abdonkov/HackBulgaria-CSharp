using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayRanges
{
    class Program
    {
        static List<int> BirthdayRanges(List<int> birthdays, List<KeyValuePair<int, int>> ranges)
        {
            List<int> birthdaysInRange = new List<int>(ranges.Count);

            foreach (KeyValuePair<int, int> range in ranges)
            {
                int timesFound = 0;
                foreach (int date in birthdays)
                {
                    if (date >= range.Key && date <= range.Value) timesFound++;
                }
                birthdaysInRange.Add(timesFound);
            }
            return birthdaysInRange;
        }

        static void Main(string[] args)
        {
            List<int> birthdays1 = new List<int>() { 5, 10, 6, 7, 3, 4, 5, 11, 21, 300, 15 };
            List<KeyValuePair<int, int>> ranges1 = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(4, 9), 
                                                                                        new KeyValuePair<int, int>(6, 7),
                                                                                        new KeyValuePair<int, int>(200, 225),
                                                                                        new KeyValuePair<int, int>(300, 365)};
            List<int> birthdays2 = new List<int>() { 1, 2, 3, 4, 5 };
            List<KeyValuePair<int, int>> ranges2 = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(1, 2), 
                                                                                        new KeyValuePair<int, int>(1, 3),
                                                                                        new KeyValuePair<int, int>(1, 4),
                                                                                        new KeyValuePair<int, int>(1, 5),
                                                                                        new KeyValuePair<int, int>(4, 6)};

            List<int> result1 = BirthdayRanges(birthdays1, ranges1);
            Console.Write("{");
            Console.Write(string.Join<int>(", ", result1));
            Console.WriteLine("}");

            List<int> result2 = BirthdayRanges(birthdays2, ranges2);
            Console.Write("{");
            Console.Write(string.Join<int>(", ", result2));
            Console.WriteLine("}");
            Console.ReadKey();
        }
    }
}
