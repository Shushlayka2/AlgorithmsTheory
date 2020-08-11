using System.Collections.Generic;

namespace Dominos
{
    public class CustomDictionary<T, V> : Dictionary<string, Node>
    {
        public void Insert(string key, Node value)
        {
            if (this[key].OutNodes.ContainsKey(value))
            {
                this[key].OutNodes[value]++;
            }
            else
            {
                this[key].OutNodes.Add(value, 1);
            }
            this[key].AmountOfOut++;
            value.AmountOfInp++;
        }
    }
}
