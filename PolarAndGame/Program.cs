using System;
using System.Linq;
using System.Collections.Generic;

namespace PolarAndGame
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
            var words = new List<string>();
            for (int i = 0; i < N + M; i++)
                words.Add(Console.ReadLine());
            var commonWordsAmount = words.Distinct().Count();
            Console.Write(N - commonWordsAmount + commonWordsAmount % 2 > M - commonWordsAmount ? "YES" : "NO");
        }
    }
}
