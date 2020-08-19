using System;
using lib;

namespace generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "products.dat");
            var separateChar = ';';
            var generator = new Generator(filePath, separateChar);
            generator.FillRandomly();
        }
    }
}
