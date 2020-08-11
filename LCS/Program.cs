using System;
using System.Text;

namespace LCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var A = Console.ReadLine();
            var B = Console.ReadLine();
            var F = new int[A.Length + 1, B.Length + 1];
            int i, j;
            for (i = 1; i <= A.Length; i++)
            {
                for (j = 1; j <= B.Length; j++)
                {
                    if (A[i - 1] == B[j - 1])
                    {
                        F[i, j] = F[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        F[i, j] = Math.Max(F[i - 1, j], F[i, j - 1]);
                    }
                }
            }

            Console.WriteLine("Size = " + F[A.Length, B.Length]);

            Console.WriteLine("Sequence:");
            var seq = new StringBuilder();
            i = A.Length;
            j = B.Length;
            while (i != 0 && j != 0)
            {
                if (F[i, j] == F[i - 1, j])
                {
                    i--;
                }
                else if (F[i, j] == F[i, j - 1])
                {
                    j--;
                }
                else
                {
                    seq.Append(A[i - 1]);
                    i--; j--;
                }
            }
            for (i = seq.Length - 1; i >= 0; i--)
            {
                Console.Write(seq[i]);
            }
        }
    }
}
