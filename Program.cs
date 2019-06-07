using System;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
using CityData;
using System.Linq;
namespace charp
{
    class Program
    {
        static void Main()
        {
            // Person guy = new Person("Eric", 10, 20);
            // guy.introduceYourself();

            var cityData = extractCityData(readCsvFromPath("./test.csv"));
            sortCityByRegion(cityData);
            // foreach (var city in cityData)
            //     city.printCityData();

        }

        public static void sortCityByRegion(List<City> cityData)
        {
            var regionQuery = from city in cityData
                              group city by city.Region into regions
                              select regions;
            foreach (var region in regionQuery)
            {
                Console.WriteLine(region.Key);
                foreach (var city in region)
                    city.printCityData();
            }
        }

        public static List<City> extractCityData(List<string[]> cityData)
        {
            var cities = new List<City>();
            foreach (var city in cityData)
                cities.Add(new City() { Region = city[0], State = city[1], Name = city[2], Population = city[3] });
            return cities;
        }

        /* 
         * This returns a list of String arrays from the given csv file
         */
        public static List<string[]> readCsvFromPath(string path)
        {

            var csvLines = new List<string[]>() { };
            using (var reader = new StreamReader(path))
            using (var parser = new CsvParser(reader))
            {
                var hasNext = true;
                while (true)
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
