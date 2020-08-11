using System;
using System.Collections.Generic;
using System.Linq;

namespace Searching
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            var N = int.Parse(Console.ReadLine());
            var A_Arr = (from a in Console.ReadLine().Split()
                         select int.Parse(a)).ToList();
            var M = int.Parse(Console.ReadLine());
            int counter = 0;
            var Q_Arr = (from q in Console.ReadLine().Split()
                         select (int.Parse(q), ++counter, 0)).ToList();
            Q_Arr.Sort();
            A_Arr.Sort();

            var count = 0;
            for (int i = 0; i < M; i++)
            {
                var tuple = Q_Arr[i];
                while (count < N && A_Arr[count] < tuple.Item1)
                {
                    count++;
                }
                tuple.Item3 = count;
                Q_Arr[i] = tuple;
            }

            Q_Arr.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            for (int i = 0; i < M; i++)
            {
                Console.Write(Q_Arr[i].Item3 + " ");
            }
        }
    }
}
