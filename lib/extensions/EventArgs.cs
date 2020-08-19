namespace lib
{
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref System.EventHandler<TEventArgs> eventDelegate)
        {
            System.EventHandler<TEventArgs> temp = System.Threading.Volatile.Read(ref eventDelegate);
            if (temp != null) temp(sender, e);
        }
    }
}