using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrapezoidEntireLine
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            (Node firstNode, Dictionary<(double, double), Queue<int>> trapezoidsDict) = GetData();
            var path = GeneratePath(firstNode);
            EnterTheAnswer(trapezoidsDict, path);
        }

        private (Node, Dictionary<(double, double), Queue<int>>) GetData()
        {
            var nodesDict = new CustomDictionary<double, Node>();
            var trapezoidsDict = new Dictionary<(double, double), Queue<int>>();
            using (StreamReader sr = new StreamReader("../../../Input.txt"))
            {
                var n = int.Parse(sr.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    var xAxis = (from s in sr.ReadLine().Split(' ')
                                 select int.Parse(s)).ToArray();

                    (double leftAngle, double rightAngle) = FindCos(xAxis);

                    if (trapezoidsDict.ContainsKey((leftAngle, rightAngle)))
                    {
                        trapezoidsDict[(leftAngle, rightAngle)].Enqueue(i + 1);
                    }
                    else
                    {
                        trapezoidsDict.Add((leftAngle, rightAngle), new Queue<int>());
                        trapezoidsDict[(leftAngle, rightAngle)].Enqueue(i + 1);
                    }

                    if (!nodesDict.ContainsKey(leftAngle))
                    {
                        nodesDict.Add(leftAngle, new Node(leftAngle));
                    }
                    if (!nodesDict.ContainsKey(rightAngle))
                    {
                        nodesDict.Add(rightAngle, new Node(rightAngle));
                    }
                    nodesDict.Insert(leftAngle, nodesDict[rightAngle]);
                }
            }
            if (!nodesDict.ContainsKey(0) || nodesDict[0].AmountOfOut != nodesDict[0].AmountOfInp)
            {
                Console.WriteLine("It's imposible to fill entire line!");
                Environment.Exit(0);
            }
            return (nodesDict[0], trapezoidsDict);
        }

        private (double, double) FindCos(int[] xAxis)
        {
            double leftCos = 0.0, rightCos = 0.0;
            leftCos = xAxis[0] / Math.Sqrt(1 + Math.Pow(xAxis[0], 2));
            rightCos = (xAxis[2] - xAxis[1]) / Math.Sqrt(1 + Math.Pow(xAxis[2] - xAxis[1], 2));
            return (Math.Abs(leftCos), Math.Abs(rightCos));
        }

        private Stack<(Node, Node)> GeneratePath(Node firstNode)
        {
            var tempStack = new Stack<(Node, Node)>();
            var resultStack = new Stack<(Node, Node)>();
            DoNextStep(ref firstNode, firstNode, tempStack, resultStack);
            return resultStack;
        }

        private void DoNextStep(ref Node finalNode, Node currentNode, Stack<(Node, Node)> tempStack, Stack<(Node, Node)> resultStack)
        {
            var count = currentNode.outNodes.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < currentNode.outNodes.ElementAt(i).Value; j++)
                {
                    var KeyValPair = currentNode.outNodes.ElementAt(i);
                    tempStack.Push((currentNode, KeyValPair.Key));
                    currentNode.outNodes[KeyValPair.Key]--;
                    DoNextStep(ref finalNode, KeyValPair.Key, tempStack, resultStack);
                }
            }
            if (currentNode != finalNode)
            {
                Console.WriteLine("It's imposible to fill entire line!");
                Environment.Exit(0);
            }
            if (tempStack.Count != 0)
            {
                resultStack.Push(tempStack.Pop());
                finalNode = resultStack.Peek().Item1;
            }
        }

        private void EnterTheAnswer(Dictionary<(double, double), Queue<int>> trapezoidsDict, Stack<(Node, Node)> path)
        {
            while(path.Count > 0)
            {
                var step = path.Pop();
                Console.Write(trapezoidsDict[(step.Item1.Value, step.Item2.Value)].Dequeue() + " ");
            }
        }
    }
}
