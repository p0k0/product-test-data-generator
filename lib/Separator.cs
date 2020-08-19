using System.IO;
using System;
using System.Threading;

namespace lib
{
    public class Separator
    {
        public event EventHandler<OnDataReceiveEventArgs> ReadProductHandlerSubscribers;

        public Separator(string filePath, char separatorChar)
        {
            this.separatorChar = separatorChar;
            this.filePath = filePath;
        }

        public void Run()
        {
            using (var fileStream = OpenFile(filePath))
            {
                var rowColumn = new string[3];
                var row = default(string);
                var e = new OnDataReceiveEventArgs();
                using (var streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                {
                    while ((row = streamReader.ReadLine()) != default(string))
                    {
                        rowColumn = row.Split(separatorChar);
                        e.Init(rowColumn[0], rowColumn[1], rowColumn[2]);
                        NotifySubscribers(e);
                    }
                }
            }
        }

        protected void NotifySubscribers(OnDataReceiveEventArgs e)
        {
            e.Raise(this, ref ReadProductHandlerSubscribers);
        }

        private FileStream OpenFile(string filePath)
        {            
            return new FileStream(filePath, FileMode.Open);
        }
        
        private char separatorChar;
        private string filePath;
    }
}