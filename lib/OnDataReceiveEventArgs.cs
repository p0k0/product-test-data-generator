namespace lib
{
    internal class OnDataReceiveEventArgs : System.EventArgs
    {
        public string Order;
        public string Name;
        public double Price;

        public OnDataReceiveEventArgs()
        {
            
        }

        public OnDataReceiveEventArgs(string order, string name, string price)
        {
            Order = order;
            Name = name;
            Price = System.Double.Parse(price);
        }

        public void Init(string order, string name, string price)
        {
            Order = order;
            Name = name;
            Price = System.Double.Parse(price);
        }
    }
}