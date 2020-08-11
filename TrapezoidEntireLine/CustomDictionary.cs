using System.Collections.Generic;

namespace TrapezoidEntireLine
{
    public class CustomDictionary<T, V> : Dictionary<double, Node>
    {
        public void Insert(double key, Node value)
        {
            if (this[key].outNodes.ContainsKey(value))
            {
                this[key].outNodes[value]++;
            }
            else
            {
                this[key].outNodes.Add(value, 1);
            }
            this[key].AmountOfOut++;
            value.AmountOfInp++;
        }
    }
}
