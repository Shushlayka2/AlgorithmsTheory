using System;
using System.Linq;

namespace СonveyorProj
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run(); 
        }

        public void Run()
        {
            (int N, double[] t1, double[] t2, double s1, double s2, double A, double B, double alpha, double beta) = GetParams();
            var f1 = new double[N + 1];
            var f2 = new double[N + 1];
            f1[0] = A; f2[0] = B;
            for (int i = 1; i < N + 1; i++)
            {
                f1[i] = Math.Min(f1[i - 1] + t1[i - 1], t2[i - 1] + f2[i - 1] + s2);
                f2[i] = Math.Min(f2[i - 1] + t2[i - 1], t1[i - 1] + f1[i - 1] + s1);
            }
            var result = Math.Min(f1[N] + alpha, f2[N] + beta);
            Console.WriteLine("Minimal requiring time is {0} by using the following algorithm:", result);
            var path = new int[N + 1];
            path[N] = result - alpha == f1[N] ? 1 : 2;
            for (int i = N - 1; i > 0; i--)
            {
                if (path[i + 1] == 1)
                {
                    path[i] = f1[i + 1] - t1[i] == f1[i] ? 1 : 2;
                }
                else 
                {
                    path[i] = f2[i + 1] - t2[i] == f2[i] ? 2 : 1;
                }
            }
            if (path[1] == 1)
            {
                path[0] = f1[1] - A == 0 ? 1 : 2;
            }
            else
            { 
                path[0] = f2[1] - B == 0 ? 2 : 1;
            }
            foreach (var step in path)
            {
                Console.Write(step + " ");
            }
        }

        (int, double[], double[], double, double, double, double, double, double) GetParams()
        {
            Console.WriteLine("Set N (amount of stages) value:");
            var N = int.Parse(Console.ReadLine());

            Console.WriteLine("Set the fulfilling times for each stage of first conveyor");
            var t1 = (from time in Console.ReadLine().Split()
                      select double.Parse(time)).ToArray();

            Console.WriteLine("Set the fulfilling times for each stage of second conveyor");
            var t2 = (from time in Console.ReadLine().Split()
                      select double.Parse(time)).ToArray();

            Console.WriteLine("Set the time for moving from the first conveyor to another");
            var s1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Set the time for moving from the second conveyor to another");
            var s2 = double.Parse(Console.ReadLine());

            Console.WriteLine("Set A and B (preporation time) values:");
            var paramsArr = (from param in Console.ReadLine().Split(' ')
                             select double.Parse(param)).ToArray();
            double A = paramsArr[0], B = paramsArr[1];

            Console.WriteLine("Set alpha and beta (building time) values:");
            paramsArr = (from param in Console.ReadLine().Split(' ')
                             select double.Parse(param)).ToArray();
            double alpha = paramsArr[0], beta = paramsArr[1];

            return (N, t1, t2, s1, s2, A, B, alpha, beta);
        }
    }
}
