using System;
using System.Linq;
using System.Collections.Generic;

namespace MillionaireCity
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
            (var N, var S) = (inp[0], inp[1]);
            var cities = new List<(double, int)>();
            for (int i = 0; i < N; i++)
            {
                inp = (from elem in Console.ReadLine().Split()
                       select int.Parse(elem)).ToList();
                (var x, var y, var k) = (inp[0], inp[1], inp[2]);
                cities.Add((Math.Sqrt(x * x + y * y), k));
            }
            cities.Sort();

            var population = S; var distance = 0.0;
            for (int i = 0; i < N && population < 1000000; i++)
            {
                var city = cities[i];
                population += city.Item2;
                distance = city.Item1;
            }

            Console.Write(population < 1000000 ? -1 : distance);
        }
    }
}
