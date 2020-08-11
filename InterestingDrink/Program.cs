using System;
using System.Collections.Generic;
using System.Linq;

namespace InterestingDrink
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
            var X_Arr = (from a in Console.ReadLine().Split()
                            select int.Parse(a)).ToList();
            var Q = int.Parse(Console.ReadLine());
            var M_Arr = new List<(int, int, int)>();
            for (int i = 0; i < Q; i++)
                M_Arr.Add((int.Parse(Console.ReadLine()), i, 0));

            M_Arr.Sort();
            X_Arr.Sort();

            var count = 0;
            for (int i = 0; i < Q; i++)
            {
                var tuple = M_Arr[i];
                while (count < N && X_Arr[count] <= tuple.Item1)
                {
                    count++;
                }
                tuple.Item3 = count;
                M_Arr[i] = tuple;
            }


            M_Arr.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            for (int i = 0; i < Q; i++)
                Console.WriteLine(M_Arr[i].Item3);
        }
    }
}
