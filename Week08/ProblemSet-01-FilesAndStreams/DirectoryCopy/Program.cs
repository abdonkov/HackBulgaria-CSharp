using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCopy
{
    class Program
    {
        public static void DirectoryCopy(DirectoryInfo dirToCopy, DirectoryInfo targetDir, bool copySubDirs, bool overwrite = false)
        {
            if (!targetDir.Exists)
            {
                targetDir.Create();
            }

            var filesInDir = dirToCopy.GetFiles();
            foreach (var file in filesInDir)
            {
                file.CopyTo(Path.Combine(targetDir.FullName, file.Name), overwrite);
            }

            if (copySubDirs)
            {
                var subDirs = dirToCopy.GetDirectories();
                foreach (var subDir in dirToCopy.GetDirectories())
                {
                    DirectoryCopy(subDir, new DirectoryInfo(Path.Combine(targetDir.FullName, subDir.Name)), copySubDirs, overwrite);
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Directory you want to copy: ");
            string path = Console.ReadLine();
            while (!Directory.Exists(path))
            {
                Console.WriteLine("Directory does not exist. Please write a valid directory!");
                Console.WriteLine("Directory you want to copy: ");
                path = Console.ReadLine();
            }
            var dirToCopy = new DirectoryInfo(path);
            Console.WriteLine("Directory to copy to: ");
            path = Console.ReadLine();
            var targetDir = new DirectoryInfo(path);
            Console.WriteLine("Do you want to copy subdirs? (Y/N): ");
            string result = Console.ReadLine();
            result = result.ToUpper();
            while(result != "Y" && result != "N")
            {
                Console.WriteLine("Invalid answer. Answer only with Y and N");
                Console.WriteLine("Do you want to copy subdirs? (Y/N): ");
                result = Console.ReadLine();
                result = result.ToUpper();
            }

            Console.WriteLine("Do you want to overwrite existing files? (Y/N): ");
            string result2 = Console.ReadLine();
            result2 = result2.ToUpper();
            while (result2 != "Y" && result2 != "N")
            {
                Console.WriteLine("Invalid answer. Answer only with Y and N");
                Console.WriteLine("Do you want to copy subdirs? (Y/N): ");
                result2 = Console.ReadLine();
                result2 = result2.ToUpper();
            }

            try
            {
                DirectoryCopy(dirToCopy, targetDir, result == "Y", result2 == "Y");
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
