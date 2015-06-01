using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool useTPL = true;

            var sw = new Stopwatch();

            sw.Start();

            if (useTPL)
            {
                Task[] MyTasks = new Task[100];

                for (int? i = 0; i < MyTasks.Length; i++)
                {
                    MyTasks[(int)i] = Task.Factory.StartNew((Object obj) =>
                    {
                        Thread.Sleep(10);
                        Console.WriteLine("{0} , {1}",
                            Thread.CurrentThread.ManagedThreadId,
                            obj as int?);

                    }, i);
                }

                Task.WaitAll(MyTasks);
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(10);
                    Console.WriteLine("{0} , {1}",
                            Thread.CurrentThread.ManagedThreadId,
                            i);
                }
            }
            

            sw.Stop();

            Console.WriteLine(sw.ElapsedTicks);

            Console.ReadKey();

        }
    }
}
