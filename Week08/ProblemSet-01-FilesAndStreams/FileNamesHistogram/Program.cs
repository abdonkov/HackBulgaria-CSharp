using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileNamesHistogram
{
    class Program
    {
        public static Dictionary<string, int> FileNamesHistogram(DirectoryInfo dir)
        {
            Dictionary<string, int> allFiles = new Dictionary<string, int>();
            FileNamesHistogramRecursion(dir, allFiles);
            return allFiles;
        }

        private static void FileNamesHistogramRecursion(DirectoryInfo dir, Dictionary<string, int> allFiles)
        {
            DirectoryInfo[] subDirs = null;
            FileInfo[] filesInDirectory = null;
            try
            {
                subDirs = dir.GetDirectories();
                filesInDirectory = dir.GetFiles();
            }
            catch (UnauthorizedAccessException)
            {

            }

            if (filesInDirectory != null)
            {
                foreach (var file in filesInDirectory)
                {
                    if (!allFiles.ContainsKey(file.Name))
                        allFiles.Add(file.Name, 1);
                    else
                        allFiles[file.Name]++;
                }
            }

            if (subDirs != null)
            {
                foreach (var subDir in subDirs)
                {
                    FileNamesHistogramRecursion(subDir, allFiles);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Directory path: ");
            string path = Console.ReadLine();
            while (!Directory.Exists(path))
            {
                Console.WriteLine("Directory does not exist. Please write a valid directory!");
                Console.WriteLine("Directory path: ");
                path = Console.ReadLine();
            }
            var dir = new DirectoryInfo(path);
            var allFiles = FileNamesHistogram(dir);
            foreach (var file in allFiles)
            {
                Console.WriteLine($"{file.Key} : {file.Value} time(s).");
            }

            Console.ReadKey();
        }
    }
}
