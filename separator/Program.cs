using System;
using lib;

namespace separator
{
    class Program
    {
        
        // 1 create separator instance 
        // 2 create bucketManager instance
        // 3 register separator at bucketManager
        // 4 run separator
        // 5 bucketManager store collected data
        static void Main(string[] args)
        {
            var inputFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "products.dat");
            var outputFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "sorted-product");
            var separatorChar = ';';
            var separator = new Separator(inputFilePath, separatorChar);
            var bucketManager = new BucketManager();
            
            bucketManager.RegisterProductPublisher(separator);
            separator.Run();

            bucketManager.PersistCollectedData(outputFilePath, separatorChar);
        }
    }
}
