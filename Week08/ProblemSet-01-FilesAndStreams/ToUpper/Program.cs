using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUpper
{
    class Program
    {
        public static void NewTextFileToUpper(FileInfo file, FileInfo newFile)
        {
            using (StreamReader sr = new StreamReader(file.FullName))
            {
                using (StreamWriter sw = new StreamWriter(newFile.FullName))
                {
                    sw.WriteLine(sr.ReadLine().ToUpper());
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("File to convert to upper: ");
            string filePath = Console.ReadLine();
            while (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist. Please write a valid file path!");
                Console.WriteLine("File to convert to upper: ");
                filePath = Console.ReadLine();
            }
            FileInfo file = new FileInfo(filePath);
            Console.WriteLine("New file: ");
            filePath = Console.ReadLine();
            FileInfo newFile = new FileInfo(filePath);

            try
            {
                NewTextFileToUpper(file, newFile);
                Console.WriteLine("Operation completed successfully!");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Could not complete operation!");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
