using System;
using System.IO;
namespace FileSplit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 3)
            {
                PrintUsage();
                return;
            }
            var arguments = new FileSplitArguments(args[0], args[1], args[2]);
            if (!arguments.IsValid())
            {
                PrintUsage();
                return;
            }
            string filename = Path.GetFileNameWithoutExtension(arguments.FileName);
            string extension = Path.GetExtension(arguments.FileName);
            //Writing Files
            using StreamReader sr = new StreamReader(arguments.FileName);
            bool done = false;
            for (int fileNumber = 1; !done; fileNumber++)
            {
                using StreamWriter sw = new StreamWriter(arguments.OutputPath + filename + "-" + fileNumber + extension);
                Console.WriteLine("Writing File: " + filename + "-" + fileNumber + extension);
                for (int lineNumber = 0; lineNumber < arguments.NumberOfLines; lineNumber++)
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
        }
        static void PrintUsage()
        {
            Console.WriteLine("Usage: FileSplit.exe <file path> <output path> <number of lines>");
        }
    }
}
