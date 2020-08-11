using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dominos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            (CustomDictionary<string, Node> nodesDict, Dictionary<(string, string), Queue<int>> dominosDict) = GetData();
            var firstNode = CompleteToCycle(nodesDict);
            if (nodesDict.Count == 0)
            {
                Environment.Exit(0);
            }
            var path = GeneratePath(firstNode == null ? nodesDict.FirstOrDefault().Value : firstNode);
            EnterTheAnswer(dominosDict, path, firstNode != null);
        }

        private (CustomDictionary<string, Node>, Dictionary<(string, string), Queue<int>>) GetData()
        {
            var nodesDict = new CustomDictionary<string, Node>();
            var dominosDict = new Dictionary<(string, string), Queue<int>>();
            using (StreamReader sr = new StreamReader("../../../input.txt"))
            {
                var s = ""; var i = 1;
                while ((s = sr.ReadLine()) != null)
                {
                    var arr = s.Split(' ');
                    string left = arr[0], right = arr[1];
                    if (dominosDict.ContainsKey((left, right)))
                    {
                        dominosDict[(left, right)].Enqueue(i);
                    }
                    else
                    {
                        dominosDict.Add((left, right), new Queue<int>());
                        dominosDict[(left, right)].Enqueue(i);
                    }
                    if (left != right)
                    {
                        if (dominosDict.ContainsKey((right, left)))
                        {
                            dominosDict[(right, left)].Enqueue(i);
                        }
                        else
                        {
                            dominosDict.Add((right, left), new Queue<int>());
                            dominosDict[(right, left)].Enqueue(i);
                        }
                    }

                    if (!nodesDict.ContainsKey(left))
                    {
                        nodesDict.Add(left, new Node(left));
                    }
                    if (!nodesDict.ContainsKey(right))
                    {
                        nodesDict.Add(right, new Node(right));
                    }
                    nodesDict.Insert(left, nodesDict[right]);
                    nodesDict.Insert(right, nodesDict[left]);
                    i++;
                }
            }
            return (nodesDict, dominosDict);
        }

        private Node CompleteToCycle(CustomDictionary<string, Node> nodesDict)
        {
            Node firstNode = null, lastNode = null;
            foreach (var node in nodesDict.Values)
            {
                if (node.AmountOfOut % 2 != 0)
                {
                    if (firstNode != null && lastNode != null)
                    {
                        Console.WriteLine("It's imposible to create a chain");
                        Environment.Exit(0);
                    }
                    if (firstNode == null)
                    {
                        firstNode = node;
                    }
                    else
                    { 
                        lastNode = node;
                    }
                }
            }
            if (firstNode != null || lastNode != null)
            {
                if (firstNode == null || lastNode == null)
                {
                    Console.WriteLine("It's imposible to create a chain");
                    Environment.Exit(0);
                }
                if (lastNode.OutNodes.ContainsKey(firstNode))
                {
                    lastNode.OutNodes[firstNode] ++;
                }
                else
                {
                    lastNode.OutNodes.Add(firstNode, 1);
                }
                if (firstNode.OutNodes.ContainsKey(lastNode))
                {
                    firstNode.OutNodes[lastNode]++;
                }
                else
                {
                    firstNode.OutNodes.Add(lastNode, 1);
                }
            }
            return firstNode;
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
            if (currentNode == finalNode && tempStack.Count != 0)
            {
                resultStack.Push(tempStack.Pop());
                finalNode = resultStack.Peek().Item1;
                return;
            }
            var count = currentNode.OutNodes.Count;
            for (int i = 0; i < count; i++)
            {
                while (currentNode.OutNodes.ElementAt(i).Value != 0)
                {
                    var KeyValPair = currentNode.OutNodes.ElementAt(i);
                    tempStack.Push((currentNode, KeyValPair.Key));
                    currentNode.OutNodes[KeyValPair.Key]--;
                    KeyValPair.Key.OutNodes[currentNode]--;
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

        private void EnterTheAnswer(Dictionary<(string, string), Queue<int>> dominosDict, Stack<(Node, Node)> path, bool isCompleted)
        {
            while (path.Count > 1)
            {
                var step = path.Pop();
                Console.Write(dominosDict[(step.Item1.Value, step.Item2.Value)].Dequeue() + " ");
            }
            if (!isCompleted)
            {
                var step = path.Pop();
                Console.Write(dominosDict[(step.Item1.Value, step.Item2.Value)].Dequeue());
            }
        }
    }
}
