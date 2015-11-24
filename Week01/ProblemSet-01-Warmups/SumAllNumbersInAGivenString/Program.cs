using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumAllNumbersInAGivenString
{
    class Program
    {
        static int SumOfNumbersInString(string str)
        {
            bool lastIsDigit = false;
            int curNum = -1;
            int sum = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                {
                    if (lastIsDigit)
                    {
                        curNum = curNum * 10 + int.Parse(str[i].ToString());
                    }
                    else
                    {
                        lastIsDigit = true;
                        curNum = int.Parse(str[i].ToString());
                    }
                }
                else
                {
                    lastIsDigit = false;
                    if (curNum != -1)
                    {
                        sum += curNum;
                        curNum = -1;
                    }
                }
            }
            if (curNum != -1) sum += curNum;
            return sum;
        }

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(SumOfNumbersInString(input));
            Console.ReadKey();
        }
    }
}
