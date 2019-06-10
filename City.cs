using System;
namespace CityData
{

    public class Simplified_City
    {
        public string name { get; set; }
        public int population { get; set; }
    }
    public class City
    {


        public string Region { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

        public Simplified_City getSimplifiedCityData()
        {
            return new Simplified_City() { name = this.Name, population = this.Population };
        }

        public void printCityData()
        {
            Console.WriteLine($"{this.Name} - {this.Population} | {this.State} {this.Region} ");
        }
    }
}

