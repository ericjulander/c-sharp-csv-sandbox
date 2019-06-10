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
        private class State
        {
            public string name { get; set; }
            public List<Simplified_City> cities { get; set; }

        }
        private class Region
        {
            public string name { get; set; }
            public List<State> states { get; set; }

        }
        static void Main()
        {
            // Person guy = new Person("Eric", 10, 20);
            // guy.introduceYourself();

            var cityData = extractCityData(readCsvFromPath("./all.csv"));
            var regions = sortCityData(cityData);

            var JSONFILE = JsonConvert.SerializeObject(regions);
            System.IO.File.WriteAllText("./output.json", JSONFILE);


        }

        private static List<Region> sortCityData(List<City> cityData)
        {
            var sortedRegions = new List<Region>();
            var regionQuery = from city in cityData
                              group city by city.Region into regions
                              select regions;
            foreach (var region in regionQuery)
            {
                var tmpList = new List<City>();
                var RegionName = region.Key;
                foreach (var city in region)
                {
                    tmpList.Add(city);
                    // city.printCityData();
                }

                sortedRegions.Add(new Region() { name = RegionName, states = sortCitiesByState(tmpList) });
            }

            return sortedRegions;
        }

        private static List<State> sortCitiesByState(List<City> region)
        {
            var stateList = new List<State>();
            var stateQuery = from city in region group city by city.State into states select states;
            var firstTime = true;
            foreach (var state in stateQuery)
            {
                var tmpList = new List<Simplified_City>();
                var State_Name = (state.Key);
                foreach (var city in state)
                {
                    if (firstTime)
                    {

                        firstTime = false;
                    }
                    tmpList.Add(city.getSimplifiedCityData());
                }
                stateList.Add(new State() { name = State_Name, cities = sortCityCityByPopulation(tmpList) });
            }
            return stateList;
        }

        public static List<Simplified_City> sortCityCityByPopulation(List<Simplified_City> cityData)
        {
            var sortedCityQuery = from cities in cityData.OrderBy(city => city.population) select cities;
            return sortedCityQuery.ToList();

        }

        public static List<City> extractCityData(List<string[]> cityData)
        {
            var cities = new List<City>();
            foreach (var city in cityData)
            {
                var population = 0;
                int.TryParse(city[3], out population);
                cities.Add(new City() { Region = city[0], State = city[1], Name = city[2], Population = population });
            }
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
