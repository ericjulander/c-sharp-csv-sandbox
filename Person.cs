using System;
namespace charp
{
    public class Person
    {
        public int age;
        public int valueAsAHuman;
        public string name;
        static void speak()
        {
            Console.WriteLine("SUP DAWG!");
        }

        public Person(string Name, int ValueAsAHuman, int Age)
        {
            this.name = Name;
            this.valueAsAHuman = ValueAsAHuman;
            this.age = Age;
        }

        public void introduceYourself()
        {
            var message = $"Hello, my name is {this.name}.\n I am {this.age} years old.\n My value as a human is {this.valueAsAHuman}/100!";
        }
    }
}