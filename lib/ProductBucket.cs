using System.Collections.Generic;

namespace lib
{
    internal class ProductBucket
    {
        public double MinPrice { get; }
        public double MaxPrice { get; }

        public ProductBucket(double minPrice, double maxPrice)
        {
            this.products = new List<Product>();
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public void Add(Product newProduct)
        {
            if (MinPrice <= newProduct.Price &&
                            newProduct.Price <= MaxPrice)
            {
                this.products.Add(newProduct);
            }
        }

        public void WriteAtFile(string filePath, char separatorChar)
        {
            this.products.Sort();
            using (var streamWriter = new System.IO.StreamWriter(filePath))
            {
                this.products.ForEach(product => product.Visit(streamWriter, separatorChar));
            }
        }

        private List<Product> products;
    }
}