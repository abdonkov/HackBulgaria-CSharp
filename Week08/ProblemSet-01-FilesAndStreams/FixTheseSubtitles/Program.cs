using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixTheseSubtitles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Open subtitles to change encoding: ");
            string filePath = Console.ReadLine();
            while (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist. Please write a valid file path!");
                Console.WriteLine("Open subtitles to change encoding: ");
                filePath = Console.ReadLine();
            }
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Program will terminate!");
                Console.ReadKey();
                return;
            }

            fileStream.Close();

            string fixedFilePath = Path.Combine(Path.GetDirectoryName(filePath),
                                                Path.GetFileNameWithoutExtension(filePath)
                                                + "-fixed" + Path.GetExtension(filePath));

            string fileText = File.ReadAllText(filePath, Encoding.GetEncoding(1251));

            File.WriteAllText(fixedFilePath, fileText, Encoding.UTF8);

            Console.ReadKey();
        }
    }
}
