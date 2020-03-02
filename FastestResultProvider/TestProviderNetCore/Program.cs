using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FastestResultProvider;

namespace TestProviderNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var lstFunctions = new List<Func<int, string>> { new Repository1().Get, new Repository6().Get, new Repository2().Get, new Repository3().Get, new Repository4().Get, new Repository5().Get };
            var lstElapsed = new List<long>();
            var optometrist1 = new Stopwatch();
            optometrist1.Start();
            for (var i = 0; i < 100; i++)
            {
                var optometrist = new Stopwatch();
                optometrist.Start();
                var result = new FastestResultProvider<int, string>(lstFunctions).Provide(5);
                Console.WriteLine("Result = " + result);
                lstElapsed.Add(optometrist.ElapsedMilliseconds);
            }
            Console.WriteLine(lstElapsed.Average());
            Console.WriteLine(optometrist1.ElapsedMilliseconds / 100);
            Console.ReadLine();
        }

        public class Repository1
        {
            public string Get(int num)
            {
                Task.Delay(140).Wait();
                return "Repository1-" + num;
            }
        }

        public class Repository2
        {
            public string Get(int num)
            {
                Task.Delay(140).Wait();
                return "Repository2-" + num;
            }
        }

        public class Repository3
        {
            public string Get(int num)
            {
                Task.Delay(140).Wait();
                return "Repository3-" + num;
            }
        }

        public class Repository4
        {
            public string Get(int num)
            {
                Task.Delay(140).Wait();
                return "Repository4-" + num;
            }
        }

        public class Repository5
        {
            public string Get(int num)
            {
                Task.Delay(100).Wait();
                return "Repository5-" + num;
            }
        }

        public class Repository6
        {
            public string Get(int num)
            {
                Task.Delay(100).Wait();
                return "Repository6-" + num;
            }
        }
    }
}
