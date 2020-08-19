using System.IO;
using System;

namespace lib
{
    public class Separator
    {
        public event EventHandler<OnDataReceiveEventArgs> ReadProductHandlerSubscribers;

        public Separator(char separatorChar)
        {
            this.separatorChar = separatorChar;
        }

        public void Visit(Stream stream)
        {
            var rowColumn = new string[3];
            var row = default(string);
            var e = new OnDataReceiveEventArgs();
            using (var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8))
            {
                while ((row = streamReader.ReadLine()) != default(string))
                {
                    rowColumn = row.Split(separatorChar);
                    e.Init(rowColumn[0], rowColumn[1], rowColumn[2]);
                    NotifySubscribers(e);
                }
            }
        }

        protected void NotifySubscribers(OnDataReceiveEventArgs e)
        {
            e.Raise(this, ref ReadProductHandlerSubscribers);
        }
        
        private char separatorChar;
    }
}