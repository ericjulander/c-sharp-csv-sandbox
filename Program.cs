using System;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
using CityData;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace charp
{
    class Program
    {
        static void Main()
        {
            // Person guy = new Person("Eric", 10, 20);
            // guy.introduceYourself();

            var cityData = extractCityData(readCsvFromPath("./test.csv"));
            var regions = sortCityByRegion(cityData);
            foreach (var region in regions)
            {
                var states = sortCitiesByState(region);
                for (var i = 0; i < states.Count; i++)
                {
                    states[i] = sortCityCityByPopulation(states[i]);
                    foreach (var city in states[i])
                    {
                        JArray jsonCity = new JArray();

                        city.printCityData();
                    }
                }

            }
            // foreach (var city in cityData)
            //     city.printCityData();

        }

        public static List<List<City>> sortCityByRegion(List<City> cityData)
        {
            var sortedRegions = new List<List<City>>();
            var regionQuery = from city in cityData
                              group city by city.Region into regions
                              select regions;
            foreach (var region in regionQuery)
            {
                var tmpList = new List<City>();
                foreach (var city in region)
                {
                    tmpList.Add(city);
                    city.printCityData();
                }

                sortedRegions.Add(tmpList);
            }

            return sortedRegions;
        }

        public static List<List<City>> sortCitiesByState(List<City> region)
        {
            var stateList = new List<List<City>>();
            var stateQuery = from city in region group city by city.State into states select states;
            var firstTime = true;
            foreach (var state in stateQuery)
            {
                var tmpList = new List<City>();
                ;
                foreach (var city in state)
                {
                    if (firstTime)
                    {

                        firstTime = false;
                    }
                    tmpList.Add(city);
                    stateList.Add(tmpList);
                }
            }
            return stateList;
        }

        public static List<City> sortCityCityByPopulation(List<City> cityData)
        {
            var sortedCityQuery = from cities in cityData.OrderBy(city => city.Population) select cities;
            return sortedCityQuery.ToList();

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
