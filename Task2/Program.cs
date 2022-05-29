using System;
using System.IO;
using System.Threading;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            object lockObj = new object();

            Thread thread2 = new Thread(() => ReadAndWrite("File2.txt", "File3.txt", lockObj));
            Thread thread1 = new Thread(() => ReadAndWrite("File1.txt", "File3.txt", lockObj));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void ReadAndWrite(string from, string to, object lockObj)
        {
            FileInfo fileInfoFrom = new FileInfo(from);
            FileInfo fileInfoTo = new FileInfo(to);
            StreamWriter writer = null;
            StreamReader reader = null;
            lock (lockObj)
            {
                Thread.Sleep(100);
                try
                {
                    reader = fileInfoFrom.OpenText();
                    writer = new StreamWriter(fileInfoTo.Open(FileMode.Append, FileAccess.Write));
                    writer.WriteLine($"{reader.ReadToEnd()} - { DateTime.Now}");
                    writer.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    reader?.Close();
                    writer?.Close();
                }
            }
        }
    }
}
