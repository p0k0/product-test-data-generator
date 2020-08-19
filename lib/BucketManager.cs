using System;
using System.Collections.Generic;

namespace lib
{
    public class BucketManager
    {
        private readonly Dictionary<int, ProductBucket> bucketsMap;
        private int bucketIndex = 0;

        public BucketManager()
        {
            bucketsMap = new Dictionary<int, ProductBucket>
            {
                [0] = new ProductBucket(1.0, 100.0),
                [1] = new ProductBucket(101.0, 200.0),
                [2] = new ProductBucket(201.0, 300.0),
                [3] = new ProductBucket(301.0, 400.0),
                [4] = new ProductBucket(401.0, 500.0),
                [5] = new ProductBucket(501.0, 600.0),
                [6] = new ProductBucket(601.0, 700.0),
                [7] = new ProductBucket(701.0, 800.0),
                [8] = new ProductBucket(801.0, 900.0),
                [9] = new ProductBucket(901.0, 1000.0),
            };
        }

        public void PersistCollectedData(string[] outputFiles, char separatorChar)
        {
            foreach (var (k,v) in bucketsMap)
            {
                v.WriteAtFile(outputFiles[k], separatorChar);
            }
        }

        public void ProductReciveHandler(object sender, OnDataReceiveEventArgs e)
        {
            bucketIndex = GetBucketIndex(e.Price);
            bucketsMap[bucketIndex].Add(new Product(e.Order, e.Name, e.Price));
        }

        public void RegisterProductPublisher(Separator separator)
        {
            this.separator = separator;
            separator.ReadProductHandlerSubscribers += ProductReciveHandler;
        }

        private Separator separator;

        private int GetBucketIndex(double price)
        {
            double value = Math.Floor(price / 100.0);
            return Convert.ToInt32(value);
        }
    }
}