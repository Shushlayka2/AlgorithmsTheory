using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace KargerAlgorithm
{
    public class CustomDictionary<K, V> : Dictionary<int, List<int>>, ISerializable
    {
        public CustomDictionary()
            :base()
        { }

        private CustomDictionary(int capacity, IEqualityComparer<int>? comparer)
            :base(capacity, comparer)
        { }

        public void Insert(int key, int value)
        {
            if (this.ContainsKey(key))
            {
                this[key].Add(value);
            }
            else
            {
                this.Add(key, new List<int> { value });
            }
        }

        public CustomDictionary<int, List<int>> Clone()
        {
            var ret = new CustomDictionary<int, List<int>>(this.Count, this.Comparer);
            foreach (KeyValuePair<int, List<int>> entry in this)
            {
                ret.Add(entry.Key, GenericCopier<List<int>>.DeepCopy(entry.Value));
            }
            return ret;
        }
    }
    public static class GenericCopier<T>
    {
        public static T DeepCopy(object objectToCopy)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, objectToCopy);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}
