using System;
namespace CityData
{

    public class City
    {
        public string Region { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string Population { get; set; }

        public void printCityData()
        {
            Console.WriteLine($"{this.Name} - {this.Population} | {this.State} {this.Region} ");
        }
    }
}