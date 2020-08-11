using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CitiesNames
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            var cities = GetCitiesList();
            var relationMap = new int[26, 26];
            var nodesList = new int[26];
            var citiesDict = new Dictionary<(int, int), Queue<string>>();
            foreach (var origName in cities)
            {
                var city = origName.ToLower();
                relationMap[city[0] - 'a', city[city.Length - 1] - 'a']++;
                nodesList[city[0] - 'a'] = 1;
                nodesList[city[city.Length - 1] - 'a'] = 1;
                if (citiesDict.ContainsKey((city[0] - 'a', city[city.Length - 1] - 'a')))
                {
                    citiesDict[(city[0] - 'a', city[city.Length - 1] - 'a')].Enqueue(origName);
                }
                else
                {
                    citiesDict.Add((city[0] - 'a', city[city.Length - 1] - 'a'), new Queue<string>());
                    citiesDict[(city[0] - 'a', city[city.Length - 1] - 'a')].Enqueue(origName);
                }
            }
            var firstNode = CompleteRelationMap(relationMap, nodesList);

            var path = GeneratePath(relationMap, firstNode == -1 ? cities.First().ToLower()[0] - 'a' : firstNode);
            EnterTheAnswer(citiesDict, path, firstNode);
        }

        private List<string> GetCitiesList()
        {
            var result = new List<string>();
            using (StreamReader sr = new StreamReader("../../../Cities.txt"))
            {
                while (!sr.EndOfStream)
                {
                    result.Add(sr.ReadLine());
                }
            }
            return result;
        }

        private int CompleteRelationMap(int[,] relationMap, int[] nodesList)
        {
            int firstNode = -1, lastNode = -1;
            for (int i = 0; i < 26; i++)
            {
                if (nodesList[i] == 1)
                {
                    int inpEdgesCount = 0, outEdgesCount = 0;
                    for (int j = 0; j < 26; j++)
                    {
                        outEdgesCount += relationMap[i, j];
                        inpEdgesCount += relationMap[j, i];
                    }
                    if (inpEdgesCount < outEdgesCount)
                    {
                        if (firstNode != -1)
                        {
                            Console.WriteLine("It's imposible to use all the cities!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            firstNode = i;
                        }
                    }
                    else if (inpEdgesCount > outEdgesCount)
                    {
                        if (lastNode != -1)
                        {
                            Console.WriteLine("It's imposible to use all the cities!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            lastNode = i;
                        }
                    }
                }
            }
            if (firstNode != -1 || lastNode != -1)
            {
                if (firstNode == -1 || lastNode == -1)
                {
                    Console.WriteLine("It's imposible to use all the cities!");
                    Environment.Exit(0);
                }
                relationMap[lastNode, firstNode]++;
            }
            return firstNode;
        }

        private Stack<(int, int)> GeneratePath(int[,] relationMap, int firstNode)
        {
            var tempStack = new Stack<(int, int)>();
            var resultStack = new Stack<(int, int)>();
            DoNextStep(relationMap, ref firstNode, firstNode, tempStack, resultStack);
            return resultStack;
        }

        private void DoNextStep(int[,] relationMap, ref int finalNode, int currentNode, Stack<(int, int)> tempStack, Stack<(int, int)> resultStack)
        {
            if (currentNode == finalNode && tempStack.Count != 0)
            {
                resultStack.Push(tempStack.Pop());
                finalNode = resultStack.Peek().Item1;
                return;
            }
            for (int i = 0; i < 26; i++)
            {
                if (relationMap[currentNode, i] != 0)
                { 
                    while (relationMap[currentNode, i] != 0)
                    {
                        tempStack.Push((currentNode, i));
                        relationMap[currentNode, i]--;
                        DoNextStep(relationMap, ref finalNode, i, tempStack, resultStack);
                    }
                }
            }
            if (currentNode != finalNode)
            {
                Console.WriteLine("It's imposible to use all the cities!");
                Environment.Exit(0);
            }
            if (tempStack.Count != 0)
            {
                resultStack.Push(tempStack.Pop());
                finalNode = resultStack.Peek().Item1;
            }
        }

        private void EnterTheAnswer(Dictionary<(int, int), Queue<string>> citiesDict, Stack<(int, int)> path, int firstNode)
        {
            if (citiesDict.Count > path.Count)
            {
                Console.WriteLine("It's imposible to use all the cities!");
                Environment.Exit(0);
            }
            while (path.Count != 1)
            {
                Console.WriteLine(citiesDict[path.Pop()].Dequeue());
            }
            if (firstNode == -1)
            {
                Console.WriteLine(citiesDict[path.Pop()].Dequeue());
            }
        }
    }
}
