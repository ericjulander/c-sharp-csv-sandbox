using System;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
namespace charp
{
    class Program
    {
        static void Main()
        {
            var rdmTestStringz = readCsvFromPath("./test.csv");
            foreach (var nameList in rdmTestStringz)
            {
                var message = "";
                foreach (var name in nameList)
                {
                    message += ($"{name} | ");
                }
                Console.WriteLine(message);
            }

        }
        /* 
         * This returns a list of String arrays from the given csv file
         */
        public static List<string[]> readCsvFromPath(string path)
        {
            var _3_ = 2;
            var csvLines = new List<string[]>() { };
            using (var reader = new StreamReader(path))
            using (var parser = new CsvParser(reader))
            {
                var hasNext = true;
                while (1 + 1 == _3_)
                {
                    var records = parser.Read();
                    hasNext = (records != null);
                    if (!hasNext) break;
                    csvLines.Add(records);

                }
            }
            return csvLines;
        }
    }

}
