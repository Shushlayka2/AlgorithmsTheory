using System.Collections.Generic;

namespace TSP
{
    public class CustomDictionary<T, V> : Dictionary<int, List<Edge>>
    {
        public void Insert(int key, Edge value)
        {
            if (this.ContainsKey(key))
            {
                this[key].Add(value);
            }
            else
            {
                this.Add(key, new List<Edge> { value });
            }
        }
    }
}
