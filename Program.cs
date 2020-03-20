using System;
using System.IO;
namespace FileSplit
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args == null || args.Length != 3)
            {
                PrintUsage();
                return 1;
            }
            // Set Number of Lines
            int numberOfLines;
            try
            {
                numberOfLines = int.Parse(args[2]);
            }
            catch
            {
                Console.WriteLine("ERROR: Could not convert number of lines to number: " + args[2]);
                PrintUsage();
                return 1;
            }
            if (numberOfLines == 0)
            {
                Console.WriteLine("ERROR: Can not set 0 as number of lines");
                PrintUsage();
                return 1;
            }

            // Check File Exists
            string filepath = args[0];
            if (!File.Exists(filepath))
            {
                Console.WriteLine("ERROR: Can not find file: "+ filepath);
                return 1;
            }
            string filename = Path.GetFileNameWithoutExtension(filepath);
            string extension = Path.GetExtension(filepath);
            
            // Check Ouput Path Exists
            string outputpath = args[1];
            Directory.CreateDirectory(outputpath);

            //Writing Files
            using StreamReader sr = new StreamReader(filepath);
            bool done = false;
            for (int fileNumber = 1; !done; fileNumber++)
            {
                using StreamWriter sw = new StreamWriter(outputpath + filename + "-" + fileNumber + extension);
                Console.WriteLine("Writing File: " + filename + "-" + fileNumber + extension);
                for (int lineNumber = 0; lineNumber < numberOfLines; lineNumber++)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        done = true;
                        break;
                    }
                    sw.WriteLine(line);
                }
            }
            return 0;
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: FileSplit.exe <file path> <output path> <number of lines>");
        }
    }
}
