using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinNodesGraph
{
    public class Program
    {
        const string Path = "../../../Input.txt"; 

        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            var Edges = GetData();
            var ResultSet = new List<int>();
            while (Edges.Count != 0)
            {
                var edge = Edges.First();
                Edges.RemoveWhere(elem => elem.Item1 == edge.Item1 || elem.Item1 == edge.Item2 ||
                elem.Item2 == edge.Item1 || elem.Item2 == edge.Item2);
                ResultSet.Add(edge.Item1);
                ResultSet.Add(edge.Item2);
            }

            GiveResult(ResultSet);
        }

        private HashSet<(int, int)> GetData()
        {
            var Edges = new HashSet<(int, int)>();
            using (StreamReader sr = new StreamReader(Path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var nodes = (from elem in line.Split(' ')
                                 select int.Parse(elem)).ToArray();
                    Edges.Add((nodes[0], nodes[1]));
                    Edges.Add((nodes[1], nodes[0]));
                }
            }
            return Edges;
        }

        private void GiveResult(List<int> ResultSet)
        {
            foreach (var node in ResultSet)
            {
                Console.Write(node + " ");
            }
        }
    }
}
