using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSplit
{
    public class FileSplitArguments
    {
        public string FileName { get; private set; }
        public string OutputPath { get; private set; }
        public int NumberOfLines { get; private set; }
        public FileSplitArguments(string fileName, string outputPath, string numberOfLines)
        {
            FileName = fileName;
            OutputPath = outputPath;
            Directory.CreateDirectory(OutputPath);
            NumberOfLines = int.Parse(numberOfLines);
        }
        public bool IsValid()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
            {
                Console.WriteLine("ERROR: Could not access input file: " + FileName);
                isValid = false;
            }
            if (string.IsNullOrEmpty(OutputPath) || !Directory.Exists(OutputPath))
            {
                Console.WriteLine("ERROR: Could not access output folder: " + OutputPath);
                isValid = false;
            }
            if (NumberOfLines == 0)
            {
                Console.WriteLine("ERROR: Number of lines can not be 0");
                isValid = false;
            }
            return isValid;
        }
    }
}
