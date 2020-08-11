using System;
using System.Collections.Generic;
using System.Linq;

namespace NMeasureIntegral
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
            var M = double.Parse(Console.ReadLine());
            var random = new Random();
            var counter = 0;
            double max = 0.0;
            for (int i = 0; i < 1_000_000; i++)
            {
                var parameters = new List<double>();
                for (int j = 0; j < N; j++)
                {
                    parameters.Add(random.NextDouble());
                }
                var randomY = random.NextDouble() * M;
                var realY = RunFunction(parameters);
                if (realY > max)
                    max = realY;
                if (randomY < realY)
                    counter++;
            }

            Console.WriteLine("result = " + counter / 1_000_000.0 * M + "\nMax real Y = " + max);
        }

        public double RunFunction(List<double> parameters)
        {
            return parameters[0] / parameters.Sum();
        }
    }
}
