using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RandomNumbers
{
    class Program
    {
        static void GenerateRandomMatrix(int rows, int columns, string fileName)
        {
            
            string[] lines = new string[rows];
            Random rand = new Random();

            for (int i = 0; i < rows; i++)
            {
                lines[i] = "";
                for (int j = 0; j < columns; j++)
                {
                    float randFloat = (float)(rand.NextDouble() * 1000.8);
                    if (randFloat > 1000) randFloat = 1000;
                    lines[i] += string.Format("{0,8:F2}", randFloat);
                }
            }
            try
            {
                File.WriteAllLines(fileName, lines);
                Console.WriteLine("File written successfuly!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            GenerateRandomMatrix(rows, cols, "rand.txt");
            Console.ReadKey();
        }
    }
}
