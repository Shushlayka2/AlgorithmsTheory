using System.Collections.Generic;

namespace Dominos
{
    public class Node
    {
        public string Value { get; set; }
        public int AmountOfInp { get; set; }
        public int AmountOfOut { get; set; }

        public Dictionary<Node, int> OutNodes = new Dictionary<Node, int>();

        public Node(string value)
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
