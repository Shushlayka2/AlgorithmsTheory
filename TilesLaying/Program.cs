using System;

namespace TilesLaying
{
    class Program
    {
        static void Main(string[] args)
        {
            var length = Int32.Parse(Console.ReadLine());
            int prevA = 3, prevB = 1, result = 0;
            switch (length)
            {
                case 0:
                    result = 1;
                    break;
                case 1:
                case 3:
                    result = 0;
                    break;
                case 2:
                    result = 3;
                    break;
            }

            for (int i = 4; i <= length; i ++)
            {
                if (i % 2 == 0)
                {
                    result = 3 * prevA + 2 * prevB;
                }
                else
                {
                    prevB = prevA + prevB;
                    prevA = result;
                    result = 0;
                }
            }
            Console.WriteLine(result);
        }
    }
}
