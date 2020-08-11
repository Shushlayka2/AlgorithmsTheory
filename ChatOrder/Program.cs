using System;
using System.Linq;
using System.Collections.Generic;

namespace ChatOrder
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
            var messages = new List<string>();
            for (int i = 0; i < N; i++)
                messages.Add(Console.ReadLine());
            
            messages.Reverse();
            messages = messages.Distinct().ToList();

            foreach (var person in messages)
                Console.WriteLine(person);
        }
    }
}
