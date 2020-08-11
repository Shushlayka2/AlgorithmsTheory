using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace HappyTicket
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            var digits = GetData();
            var N = digits.Sum();
            if (N % 2 != 0)
            {
                Console.WriteLine("It's impossible!");
                Environment.Exit(0);
            }
            N /= 2;
            var arr = new int[digits.Count + 2, N + 1];
            int i;
            for (i = 0; i < digits.Count + 2; i++)
            { 
                arr[i, 0] = 1;
            }
            i = 1;
            foreach (var digit in digits)
            {
                for (int j = 0; j <= N; j++)
                {
                    if (arr[i - 1, j] != 0 && j + digit <= N)
                    {
                        arr[i, j + digit]++;
                        arr[i + 1, j + digit]++;
                    }
                }
                if (arr[i, N] != 0)
                {
                    PrintResult(arr, digits, N, i);
                    Environment.Exit(0);
                }
                i++;
            }
            Console.WriteLine("It's impossible!");
        }

        private List<int> GetData()
        {
            List<int> digits;
            using (StreamReader sr = new StreamReader("../../../input.txt"))
            {
                digits = (from digit in sr.ReadLine().Split(' ')
                              select int.Parse(digit)).ToList();
            }
            return digits;
        }

        private void PrintResult(int[,] arr, List<int> digits, int N, int i)
        {
            var path = new Stack<int>();
            int curN = N;
            for (; i > 0; i--)
            {
                if (arr[i - 1, curN] < arr[i, curN])
                {
                    var digit = digits[i - 1];
                    path.Push(digit);
                    curN -= digit;
                }
            }
            while (path.Count > 0) 
            {
                Console.Write(path.Pop() + " ");
            }
        }
    }
}
