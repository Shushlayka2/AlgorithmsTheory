using System;
using System.Collections.Generic;
using System.Linq;

namespace EugeneAndPlaylist
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            var inp = (from elem in Console.ReadLine().Split()
                       select int.Parse(elem)).ToList();
            (var N, var M) = (inp[0], inp[1]);
            var timeLine = new List<int>() { };
            for (int i = 0; i < N; i++)
            {
                inp = (from elem in Console.ReadLine().Split()
                       select int.Parse(elem)).ToList();
                (var c, var t) = (inp[0], inp[1]);
                timeLine.Add(timeLine.LastOrDefault() + c * t);
            }

            var V = (from elem in Console.ReadLine().Split()
                     select int.Parse(elem)).ToList();

            int num = 1;
            foreach (var v in V)
            {
                while (timeLine[num - 1] < v)
                    num++;
                Console.WriteLine(num + " ");
            }
        }
    }
}
