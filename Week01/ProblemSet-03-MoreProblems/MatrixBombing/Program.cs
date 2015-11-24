using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixBombing
{
    class Program
    {
        static int MatrixBombing(int[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            int maxDamage = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int curDamage = 0;

                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1; l++)
                        {
                            if (k != i || l != j)
                            {
                                try
                                {
                                    curDamage += m[i, j] < m[k, l] ? m[i, j] : m[k, l];
                                }
                                catch (IndexOutOfRangeException) { }
                            }
                        }
                    }
                    if (maxDamage < curDamage) maxDamage = curDamage;
                }
            }

            return maxDamage;
        }

        static void Main(string[] args)
        {
            int[,] m = new int[,] {{1, 2, 3, 4},
                                   {5, 6, 7, 8},
                                   {9, 10, 11, 12}};

            Console.WriteLine(MatrixBombing(m));
            Console.ReadKey();
        }
    }
}
