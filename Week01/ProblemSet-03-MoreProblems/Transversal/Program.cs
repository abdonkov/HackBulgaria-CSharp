using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal
{
    class Program
    {
        static bool IsTransversal(List<int> transversal, List<List<int>> family)
        {
            foreach (List<int> curList in family)
            {
                bool found = false;
                foreach (int curNum in curList)
                {
                    foreach (int transNum in transversal)
                    {
                        if (transNum == curNum)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found) break;
                }
                if (!found) return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            List<int> transversal1 = new List<int>() { 1, 4, 7 };
            List<List<int>> family1 = new List<List<int>>() { new List<int>{ 1, 2, 3 },
                                                              new List<int>{ 4, 5, 6 },
                                                              new List<int>{ 7, 8, 9 }
                                                            };
            List<int> transversal2 = new List<int>() { 4, 5, 6 };
            List<List<int>> family2 = new List<List<int>>() { new List<int>{ 5, 7, 9 },
                                                              new List<int>{ 1, 4, 3 },
                                                              new List<int>{ 2, 6 }
                                                            };
            List<int> transversal3 = new List<int>() { 1, 2 };
            List<List<int>> family3 = new List<List<int>>() { new List<int>{ 1, 5 },
                                                              new List<int>{ 2, 3 },
                                                              new List<int>{ 4, 7 }
                                                            };
            List<int> transversal4 = new List<int>() { 2, 3, 4 };
            List<List<int>> family4 = new List<List<int>>() { new List<int>{ 1, 7 },
                                                              new List<int>{ 2, 3, 5 },
                                                              new List<int>{ 4, 8 }
                                                            };

            Console.WriteLine(IsTransversal(transversal1, family1));
            Console.WriteLine(IsTransversal(transversal2, family2));
            Console.WriteLine(IsTransversal(transversal3, family3));
            Console.WriteLine(IsTransversal(transversal4, family4));
            Console.ReadKey();
        }
    }
}
