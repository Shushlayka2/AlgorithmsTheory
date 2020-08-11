using System;
using System.Linq;
using System.Collections.Generic;

namespace RepeatedDigit
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            (int[] arr, Dictionary<int, int> elems) = GenerateNums();
            var random = new Random();
            for (int i = 0; i < 20; i++)
            {
                var position = random.Next(1_000_000);
                elems[arr[position]]++;
            }
            var max = 0;
            var result = 0;
            foreach (var e in elems)
            {
                if (max < e.Value)
                {
                    max = e.Value;
                    result = e.Key;
                }
            }
            Console.WriteLine(result);
        }

        private (int[], Dictionary<int, int>) GenerateNums()
        {
            var N = int.Parse(Console.ReadLine());
            var random = new Random();
            var elemsNum = random.Next(50);
            var dictionary = new Dictionary<int, int>();
            var repetitionsCount = random.Next(500_000, 1_000_000);
            var remainedElementsCount = 1_000_000 - repetitionsCount;
            dictionary.Add(N, repetitionsCount);
            for (int i = 0; i < elemsNum - 2; i++)
            { 
                repetitionsCount = random.Next(remainedElementsCount);
                remainedElementsCount -= repetitionsCount;
                var elementLocal = random.Next(100);
                while (dictionary.ContainsKey(elementLocal))
                    elementLocal = random.Next(100);
                dictionary.Add(elementLocal, repetitionsCount);
            }
            var element = random.Next(100);
            while (dictionary.ContainsKey(element))
                element = random.Next(100);
            if (remainedElementsCount != 0)
                dictionary.Add(element, remainedElementsCount);
            else
                dictionary.Add(element, 0);
            var result = new int[1_000_000];
            var test = 0;
            foreach (var t in dictionary)
            {
                test += t.Value;
            }
            for (int i = 0; i < 1_000_000; i++)
            {
                var position = random.Next(elemsNum - 1);
                var key = dictionary.ElementAt(position).Key;
                if (dictionary[key] != 0)
                    dictionary[key]--;
                else
                {
                    int j = 0;
                    while (dictionary.ElementAt(j).Value == 0)
                        j++;
                    key = dictionary.ElementAt(j).Key;
                    dictionary[key]--;
                }
                result[i] = key;
            }
            return (result, dictionary);
        }
    }
}
