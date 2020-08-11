using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KargerAlgorithm
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();  
        }

        public void Run()
        {
            var graph = ReadData();
            var count = graph.Count * (graph.Count - 1);
            var answer = graph.Count + 1;
            for (int i = 0; i < count; i++)
            {
                var copyGraph = graph.Clone();
                var curCut = getMinCut(copyGraph);
                if (curCut < answer)
                    answer = curCut;
            }
            Console.WriteLine(answer);
        }

        private CustomDictionary<int, List<int>> ReadData()
        {
            var graph = new CustomDictionary<int, List<int>>();
            using (StreamReader sr = new StreamReader("../../../input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var nodes = (from node in sr.ReadLine().Split(' ')
                                 select int.Parse(node)).ToArray();
                    graph.Insert(nodes[0], nodes[1]);
                }
            }
            return graph;
        }

        private int getMinCut(CustomDictionary<int, List<int>> graph)
        {
            var random = new Random();
            var nextNode = graph.Count + 1;
            for (int i = graph.Count; i > 2; i--)
            {
                var firstNode = graph.ElementAt(random.Next(graph.Count)).Key;
                var secondNode = graph[firstNode][random.Next(graph[firstNode].Count)];
                graph.Add(nextNode, new List<int>());
                foreach (var neighboreNode in graph[firstNode])
                {
                    if (neighboreNode != secondNode)
                    {
                        graph[neighboreNode].Remove(firstNode);
                        graph[neighboreNode].Add(nextNode);
                        graph[nextNode].Add(neighboreNode);
                    }
                }
                foreach (var neighboreNode in graph[secondNode])
                {
                    if (neighboreNode != firstNode)
                    {
                        graph[neighboreNode].Remove(secondNode);
                        graph[neighboreNode].Add(nextNode);
                        graph[nextNode].Add(neighboreNode);
                    }
                }
                graph.Remove(firstNode);
                graph.Remove(secondNode);
                nextNode++;
            }
            return graph.ElementAt(0).Value.Count;
        }
    }
}
