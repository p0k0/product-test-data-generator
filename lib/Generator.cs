using System;
using System.IO;

namespace lib
{
    public class Generator
    {
        public Generator(char separatorChar)
        {
            this.separatorChar = separatorChar;
        }

        public void WriteRandomData(Stream stream, int rowCount = 10000)
        {
            System.Text.StringBuilder nameBuilder = new System.Text.StringBuilder();
            double price = default(double);

            using(var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8))
            {
                for(var row = 0; row < rowCount; row++)
                {
                    nameBuilder = CreateProductName(row);
                    price = CreatePrice();
                    streamWriter.Write(row);
                    
                    streamWriter.Write(separatorChar);
                    streamWriter.Write(nameBuilder);
                    streamWriter.Write(separatorChar);
                    streamWriter.WriteLine(price);
                }
            }
        }

        FileStream CreateFile(string filePath)
        {
            return new FileStream(filePath, FileMode.Create);
        }

        private double CreatePrice()
        {
            return rndGenerator.NextDouble() * 1000;
        }

        private static System.Text.StringBuilder CreateProductName(int row)
        {
            var nameBuilder = new System.Text.StringBuilder();
            nameBuilder.Clear();
            nameBuilder.Append($@"test-product-{row}");
            return nameBuilder;
        }

        private Random rndGenerator = new Random(DateTime.UtcNow.Second);
        private char separatorChar;
    }
}
