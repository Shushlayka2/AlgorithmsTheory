using System.Collections.Generic;

namespace TrapezoidEntireLine
{
    public class Node
    {
        public double Value { get; set; }
        public int AmountOfInp { get; set; }
        public int AmountOfOut { get; set; }

        public Dictionary<Node, int> outNodes = new Dictionary<Node, int>();

        public Node(double value)
        {
            Value = value;
            AmountOfInp = 0;
            AmountOfOut = 0;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Node other = obj as Node;
            return other != null && other.Value == this.Value;
        }
    }
}
