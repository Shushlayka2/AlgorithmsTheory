using System;
using System.Collections.Generic;
using System.Linq;

namespace BagTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //data obtaining
            var M = int.Parse(Console.ReadLine());
            var s = "";
            var items = new List<Item>();
            while ((s = Console.ReadLine()) != "")
            {
                var pieces = (from p in s.Split(' ')
                             select int.Parse(p)).ToArray();
                items.Add(new Item(pieces[0], pieces[1]));
            }

            //main alg handling
            var prevArr = new int[M + 1];
            var curArr = new int[M + 1];
            var tempPointer = prevArr;
            foreach (var item in items)
            {
                
                for (int i = 0; i <= M; i++)
                {
                    if (item.Weight <= i)
                    {
                        curArr[i] = Math.Max(prevArr[i], item.Worth + prevArr[i - item.Weight]);
                    }
                    else
                    {
                        curArr[i] = prevArr[i];
                    }
                }
                tempPointer = prevArr;
                prevArr = curArr;
                curArr = tempPointer;
            }

            //resulting
            Console.WriteLine(prevArr[M]);
        }
    }
}
