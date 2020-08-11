using System;

namespace TSP
{
    public class Edge : IComparable
    {
        public int Weight { get; set; }
        public int FirstNode { get; set; }
        public int SecondNode { get; set; }

        public Edge(int firstNode, int secondNode, int weight)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
            Weight = weight;
        }

        public override int GetHashCode()
        {
            return Weight.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Edge other = obj as Edge;
            return other != null && other.Weight == this.Weight;
        }

        public int CompareTo(object obj)
        {
            if (this.Weight > ((Edge)obj).Weight)
                return 1;
            if (this.Weight < ((Edge)obj).Weight)
                return -1;
            else
                return 0;
        }
    }
}
