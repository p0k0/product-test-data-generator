using System;
using System.IO;

namespace lib
{
    public class Generator
    {
        public Generator(string filePath, char separatorChar)
        {
            this.separatorChar = separatorChar;
            var fileStream = CreateFile(filePath);
            fileStream = AddData(fileStream, 10000);
        }

        FileStream CreateFile(string filePath)
        {
            if (File.Exists(filePath))
                throw new Exception("File already exists");
            
            return new FileStream(filePath, FileMode.Create);
        }

        FileStream AddData(FileStream fileStream, int rowCount)
        {
            using(var streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                foreach(var row in System.Linq.Enumerable.Range(0, rowCount))
                {
                    ReRoll();
                    AddRow(streamWriter);
                }
            }
        }

        void AddRow(StreamWriter streamWriter)
        {
            streamWriter.Write(order);
            streamWriter.Write(separatorChar);
            streamWriter.Write(name);
            streamWriter.Write(separatorChar);
            streamWriter.WriteLine(price);
        }

        void ReRoll()
        {
            order++;
            nameBuilder.Clear(); 
            nameBuilder.AppendLine($@"test-product-{order}");
            price = rndGenerator.Sample() * 1000;
        }

        private Random rndGenerator = new Random(DateTime.UtcNow.Second);
        
        private int order = -1;
        private System.Text.StringBuilder nameBuilder = new System.Text.StringBuilder();
        private double price;
        private char separatorChar;
    }
}
