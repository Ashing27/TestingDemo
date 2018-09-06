using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world. Let's do this.");

//             DateTime dt1 = DateTime.Now;
//             Thread.Sleep(10000);
//             DateTime dt2 = DateTime.Now;

            for (int i = 0; i < 5; i++)
            {
                DateTime dt = DateTime.Now;
                timeboard.Add(i, dt);
                Thread.Sleep(1000);
            }

            for (int j = 0; j < timeboard.Count(); j++)
            {
                DateTime dt2 = DateTime.Now;
                Console.WriteLine((dt2 - timeboard[j]).ToString());
            }

            Console.ReadLine();
            
        }

        static private Dictionary<int, DateTime> timeboard = new Dictionary<int, DateTime>();

    }
}
