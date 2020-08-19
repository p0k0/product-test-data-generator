using System;

namespace lib
{
    internal class Product : IComparable<Product>
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int order, string name, double price)
        {
            Order = order;
            Name = name;
            Price = price;
        }

        public void Visit(System.IO.StreamWriter outputStreamWriter, char separatorChar)
        {
            outputStreamWriter.Write(Order);
            outputStreamWriter.Write(separatorChar);
            outputStreamWriter.Write(Name);
            outputStreamWriter.Write(separatorChar);
            outputStreamWriter.WriteLine(Price);
        }

        public int CompareTo(Product other)
        {
            if (other == null) return 1;

            return this.Price.CompareTo(other.Price);
        }
    }
}