using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirectoryTraversal
{
    class Program
    {
        static IEnumerable<string> IterateDirectory(DirectoryInfo dir, LinkedList<string> indent = null, bool lastFolder = true)
        {
            if (indent == null)
            {
                indent = new LinkedList<string>();

                StringBuilder row = new StringBuilder();

                if (dir.GetFiles().Any())
                {
                    row.Append("├─");
                    indent.AddLast("| ");
                }
                else
                {
                    row.Append("└─");
                    indent.AddLast("  ");
                }

                row.Append(dir.Name);
                yield return row.ToString();
            }
            bool canAccess = true;
            try
            {
                dir.GetDirectories();
                dir.GetFiles();
            }
            catch (System.UnauthorizedAccessException)
            {
                canAccess = false;
            }

            if (canAccess)
            {
                bool lastFolderTemp = lastFolder;

                DirectoryInfo[] subDirs = dir.GetDirectories();
                
                for (int i = 0; i < subDirs.Length; i++)
                {
                    bool hasFilesAndFolders = false;
                    
                    StringBuilder row = new StringBuilder();
                    
                    row.Append(string.Concat(indent.ToArray()));

                    if (i == subDirs.Length - 1)
                    {
                        lastFolder = true;
                        try
                        {
                            if (subDirs[i].GetFiles().Any() && subDirs[i].GetDirectories().Any()) hasFilesAndFolders = true;
                        }
                        catch(UnauthorizedAccessException)
                        {
                            canAccess = false;
                            continue;
                        }
                    }
                    else
                    {
                        hasFilesAndFolders = true;
                        lastFolder = false;
                    }

                    if (hasFilesAndFolders) row.Append("├─");
                    else row.Append("└─");

                    row.Append(subDirs[i].Name);
                    yield return row.ToString();

                    if (hasFilesAndFolders) indent.AddLast("| ");
                    else indent.AddLast("  ");

                    foreach (var item in IterateDirectory(subDirs[i], indent, lastFolder))
                    {
                        yield return item;
                    }
                    indent.RemoveLast();
                }

                lastFolder = lastFolderTemp;

                LinkedList<string> fileIndent = new LinkedList<string>();

                foreach (var item in indent)
                {
                    fileIndent.AddLast(item);
                }

                string last = fileIndent.Last.Value;
                if (last == "| ")
                {
                    fileIndent.RemoveLast();
                    fileIndent.AddLast("├─");
                }
                else fileIndent.AddLast("├─");

                FileInfo[] files = dir.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    StringBuilder row = new StringBuilder();

                    if (i == files.Length - 1 && lastFolder)
                    {
                        fileIndent.RemoveLast();
                        fileIndent.AddLast("└─");
                    }

                    row.Append(string.Concat(fileIndent.ToArray()));
                    row.Append(files[i].Name);
                    yield return row.ToString();
                }
            }
            
            if (!canAccess)
            {
                StringBuilder row = new StringBuilder();

                row.Append("=============>");
                row.Append("START AS ADMINISTRATOR FOR MORE INFO...");
                yield return row.ToString();
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo myDocsDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            DirectoryInfo customDir = new DirectoryInfo(@"D:\");
            Console.WriteLine(string.Join("\n", IterateDirectory(myDocsDir)));
            Console.ReadKey();
        }
    }
}
