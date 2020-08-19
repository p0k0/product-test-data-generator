﻿using System;
using System.IO;

namespace lib
{
    public class Generator
    {
        public Generator(string filePath, char separatorChar)
        {
            this.separatorChar = separatorChar;
            this.filePath = filePath;
        }

        public void FillRandomly(int rowCount = 10000)
        {
            using(var fileStream = CreateFile(filePath))
            using(var streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                foreach(var row in System.Linq.Enumerable.Range(0, rowCount))
                {
                    GenerateRowData();
                    WriteRowData(streamWriter);
                }
            }
        }

        FileStream CreateFile(string filePath)
        {
            return new FileStream(filePath, FileMode.Create);
        }

        void WriteRowData(StreamWriter streamWriter)
        {
            streamWriter.Write(order);
            streamWriter.Write(separatorChar);
            streamWriter.Write(nameBuilder);
            streamWriter.Write(separatorChar);
            streamWriter.WriteLine(price);
        }

        void GenerateRowData()
        {
            order++;
            nameBuilder.Clear(); 
            nameBuilder.Append($@"test-product-{order}");
            price = rndGenerator.NextDouble() * 1000;
        }

        private Random rndGenerator = new Random(DateTime.UtcNow.Second);
        
        private int order = -1;
        private System.Text.StringBuilder nameBuilder = new System.Text.StringBuilder();
        private double price;
        private char separatorChar;
        private string filePath;
    }
}
