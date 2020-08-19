using System.IO;
using System;
using System.Threading;

namespace lib
{
    public class Separator
    {
        public event EventHandler<OnDataReceiveEventArgs> OnDataReciveHandler;

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
                    row = streamReader.ReadLine();
                    rowColumn = row.Split(separatorChar);
                    e.Init(rowColumn[0], rowColumn[1], rowColumn[2]);
                    OnDataRecive(e);
                }
            }
        }
        
        protected void OnDataRecive(OnDataReceiveEventArgs e)
        {
            e.Raise(this, ref OnDataReciveHandler);
        }

        private FileStream OpenFile(string filePath)
        {
            if (File.Exists(filePath))
                throw new Exception("File already exists");
            
            return new FileStream(filePath, FileMode.Open);
        }
        
        private char separatorChar;
        private string filePath;
    }
}