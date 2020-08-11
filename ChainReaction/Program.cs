using System;
using System.Linq;
using System.Collections.Generic;

namespace ChainReaction
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
            var lighthouses = new List<(int, int, int)>();
            for (int i = 0; i < N; i++)
            {
                var inp = (from elem in Console.ReadLine().Split()
                           select int.Parse(elem)).ToList();
                lighthouses.Add((inp[0], inp[0] - inp[1], 0));
            }

            lighthouses.Sort();
            var lighthousesPosition = new List<int>();
            lighthousesPosition = (from tuple in lighthouses
                                   select tuple.Item1).ToList();
            lighthouses[0] = (lighthouses[0].Item1, lighthouses[0].Item2, 1);

            for (int i = 1; i < N; i++)
            {
                var tuple = lighthouses[i];
                var count = FindPosition(lighthousesPosition, 0, N - 1, tuple.Item2);
                tuple.Item3 = (count - 1) < 0 ? 1 : lighthouses[count - 1].Item3 + 1;
                lighthouses[i] = tuple;
            }

            var maxVal = lighthouses[0].Item3;
            for (int i = 1; i < N; i++)
                if (maxVal < lighthouses[i].Item3)
                    maxVal = lighthouses[i].Item3;

            Console.WriteLine(N - maxVal);
        }

        private int FindPosition(List<int> arr, int l, int r, int elem)
        {
            if (r - l < 0)
                return l;
            var pos = (l + r) / 2;
            if (arr[pos] == elem)
                return pos;
            if (arr[pos] > elem)
                return FindPosition(arr, l, pos - 1, elem);
            else
                return FindPosition(arr, pos + 1, r, elem);
        }
    }
}