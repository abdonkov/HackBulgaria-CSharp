using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFileEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Open file to edit: ");
            string filePath = Console.ReadLine();
            while (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist. Please write a valid file path!");
                Console.WriteLine("Open file to edit: ");
                filePath = Console.ReadLine();
            }
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Program will terminate!");
                Console.ReadKey();
                return;
            }

            fileStream.Close();

            Console.WriteLine($"Editing file {Path.GetFileName(filePath)}");
            Console.WriteLine("List of commands: ");
            Console.WriteLine("list - lists the contents of the file");
            Console.WriteLine("clear - clears the contents of the file");
            Console.WriteLine("appendline - appends a new line to the file");
            Console.WriteLine("append < text > - appends the text to the file");
            Console.WriteLine("linecount - outputs the numbers of lines in the file");
            Console.WriteLine("exit - exits editor");
            Console.WriteLine();

            string input = string.Empty;
            while(input != "exit")
            {
                input = Console.ReadLine();
                string[] command = input.Split();
                if (command.Length == 0) continue;
                switch (command[0].ToLower())
                {
                    case "list":
                        {
                            Console.WriteLine("---File Content---");
                            Console.WriteLine(File.ReadAllText(filePath));
                            Console.WriteLine("------------------");
                            break;
                        }
                    case "clear":
                        {
                            File.WriteAllText(filePath, string.Empty);
                            Console.WriteLine("File creared!");
                            break;
                        }
                    case "appendline":
                        {
                            File.AppendAllText(filePath, Environment.NewLine);
                            break;
                        }
                    case "append":
                        {
                            if (command.Length < 2) Console.WriteLine("Invalid command");
                            else
                            {
                                File.AppendAllText(filePath, string.Join(" ", command.Skip(1)));
                            }
                            break;
                        }
                    case "linecount":
                        {
                            Console.WriteLine(File.ReadLines(filePath).Count());
                            break;
                        }
                    case "exit":
                        {
                            input = command[0].ToLower();
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
            
                        
        }
    }
}
