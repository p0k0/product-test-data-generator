namespace lib
{
    internal class OnDataReceiveEventArgs : System.EventArgs
    {
        public int Order;
        public string Name;
        public double Price;

        public void Init(string order, string name, string price)
        {
            Order = System.Int16.Parse(order);
            Name = name;
            Price = System.Double.Parse(price);
        }
    }
}