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
            ProcessFile(arguments);
        }

        static void ProcessFile(FileSplitArguments fileSplitArguments)
        {
            string filename = Path.GetFileNameWithoutExtension(fileSplitArguments.FileName);
            string extension = Path.GetExtension(fileSplitArguments.FileName);
            //Writing Files
            using StreamReader sr = new StreamReader(fileSplitArguments.FileName);
            bool done = false;
            for (int fileNumber = 1; !done; fileNumber++)
            {
                using StreamWriter sw = new StreamWriter(fileSplitArguments.OutputPath + filename + "-" + fileNumber + extension);
                Console.WriteLine("Writing File: " + filename + "-" + fileNumber + extension);
                for (int lineNumber = 0; lineNumber < fileSplitArguments.NumberOfLines; lineNumber++)
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
