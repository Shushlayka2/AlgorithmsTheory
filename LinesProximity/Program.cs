using System;
using System.Collections.Generic;

namespace LinesProximity
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            Console.WriteLine("Enter two strings:");
            var s1 = Console.ReadLine();
            var s2 = Console.ReadLine();
            var arr = new int[s1.Length + 1, s2.Length + 1];
            for (int i = 0; i <= s1.Length; i++)
            {
                arr[i, 0] = i;
            }
            for (int i = 0; i <= s2.Length; i++)
            {
                arr[0, i] = i;
            }
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        arr[i, j] = arr[i - 1, j - 1];
                    }
                    else
                    { 
                        var min = Math.Min(arr[i - 1, j], arr[i, j - 1]);
                        min = Math.Min(min, arr[i - 1, j - 1]);
                        arr[i, j] = min + 1;
                    }
                }
            }
            Console.WriteLine("Levenshtein distance: " + arr[s1.Length, s2.Length]);
            Console.WriteLine("Taken actions:");
            var acts = new Stack<string>();

            int l = s1.Length, k = s2.Length;
            while (l != 0 || k != 0)
            {
                if (arr[l - 1, k - 1] <= arr[l, k - 1] && arr[l - 1, k - 1] <= arr[l - 1, k])
                {
                    if (arr[l, k] == arr[l - 1, k - 1])
                    {
                        acts.Push("Letters are same");
                    }
                    else
                    {
                        acts.Push("Letter changed");
                    }
                    l--; k--;
                }
                else if (arr[l, k - 1] <= arr[l - 1, k] && arr[l, k - 1] <= arr[l - 1, k - 1])
                {
                    acts.Push("Letter added");
                    k--;
                }
                else
                {
                    acts.Push("Letter deleted");
                    l--;
                }
            }

            while (acts.Count != 0)
            {
                Console.WriteLine(acts.Pop());
            }
        }
    }
}
