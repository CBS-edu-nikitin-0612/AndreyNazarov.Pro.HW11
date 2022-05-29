using System;
using System.Threading;

namespace Task4
{
    internal class Program
    {
        static int counter = 0;

        static readonly object block = new object();

        private static void Function()
        {
            lock (block)
            {
                for (int i = 0; i < 10; ++i)
                {
                    Thread.Sleep(20);
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {++counter}");
                }
            }
        }

        static void Main()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            Console.ReadKey();
        }
    }
}