using System;
using System.Linq;

namespace VanyaAndLights
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run(); 
        }

        void Run()
        {
            var inp = (from elem in Console.ReadLine().Split()
                       select int.Parse(elem)).ToList();
            (var N, var L) = (inp[0], inp[1]);
            var A = (from elem in Console.ReadLine().Split()
                     select int.Parse(elem)).ToList();
            
            A.Sort();
            double dist, max_dist = A[0];
            for (int i = 1; i < N; i++)
            {
                dist = (A[i] - A[i - 1]) / 2.0;
                if (dist > max_dist)
                    max_dist = dist;
            }
            
            dist = L - A[N - 1];
            if (dist > max_dist)
                max_dist = dist;

            Console.Write(max_dist);
        }
    }
}
