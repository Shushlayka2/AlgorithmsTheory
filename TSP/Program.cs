using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TSP
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            (Dictionary<int, int> ComponentsDic, Queue<Edge> EdgesQueue) = GetData();
            var MSTRelationMap = KruskalAlg(ComponentsDic, EdgesQueue);
            var way = GeneratePath(MSTRelationMap);
            var finalWay = CleanWay(way);
            GiveResult(finalWay);
        }

        private (Dictionary<int, int>, Queue<Edge>) GetData()
        {
            int N;
            var ComponentsDic = new Dictionary<int, int>();
            var SortList = new List<Edge>();
            using (StreamReader sr = new StreamReader("../../../input.txt"))
            {
                N = int.Parse(sr.ReadLine());
                var K = int.Parse(sr.ReadLine());
                for (int i = 0; i < K; i++)
                {
                    var arr = (from edge in sr.ReadLine().Split(' ')
                               select int.Parse(edge)).ToArray();
                    SortList.Add(new Edge(arr[0], arr[1], arr[2]));
                }
                for (int i = 0; i < N; i++)
                { 
                    ComponentsDic.Add(i, i);
                }
            }
            SortList.Sort();
            var EdgesQueue = new Queue<Edge>(SortList);
            return (ComponentsDic, EdgesQueue);
        }

        private CustomDictionary<int, List<Edge>> KruskalAlg(Dictionary<int, int> ComponentsDic, Queue<Edge> EdgesQueue)
        {
            var MSTRelationMap = new CustomDictionary<int, List<Edge>>();
            while (EdgesQueue.Count != 0)
            {
                var edge = EdgesQueue.Dequeue();
                if (ComponentsDic[edge.FirstNode] != ComponentsDic[edge.SecondNode])
                {
                    var compNum = ComponentsDic[edge.SecondNode];
                    (from keyValPair in ComponentsDic
                     where keyValPair.Value == ComponentsDic[edge.FirstNode]
                     select keyValPair.Key).ToList().ForEach(key =>
                     {
                         ComponentsDic[key] = compNum;
                     });
                    MSTRelationMap.Insert(edge.FirstNode, edge);
                    MSTRelationMap.Insert(edge.SecondNode, new Edge(edge.SecondNode, edge.FirstNode, edge.Weight));
                }
            }
            return MSTRelationMap;
        }

        private Stack<int> GeneratePath(CustomDictionary<int, List<Edge>> MSTRelationMap)
        {
            var tempStack = new Stack<int>();
            tempStack.Push(0);
            var resultStack = new Stack<int>();
            DoNextStep(0, 0, MSTRelationMap, tempStack, resultStack);
            return resultStack;
        }

        private void DoNextStep(int finalNode, int currentNode, CustomDictionary<int, List<Edge>> MSTRelationMap, Stack<int> tempStack, Stack<int> resultStack)
        {
            while (MSTRelationMap[currentNode].Count > 0)
            {
                var edge = MSTRelationMap[currentNode][0];
                tempStack.Push(edge.SecondNode);
                MSTRelationMap[currentNode].Remove(edge);
                DoNextStep(finalNode, edge.SecondNode, MSTRelationMap, tempStack, resultStack);
            }
            if (tempStack.Count != 0)
            {
                resultStack.Push(tempStack.Pop());
                if (tempStack.Count != 0)
                    finalNode = tempStack.Peek();
            }
        }

        private List<int> CleanWay(Stack<int> way)
        {
            var result = new List<int>();
            while (way.Count > 0)
            {
                var step = way.Pop();
                if (!result.Exists(s => s == step))
                {
                    result.Add(step);
                }
            }
            return result;
        }

        private void GiveResult(List<int> way)
        {
            foreach (var step in way)
            {
                Console.Write(step + " ");
            }
        }
    }
}
