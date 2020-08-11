namespace BagTask
{
    public class Item
    {
        public int Weight { get; set; }

        public int Worth { get; set; }

        public Item(int weight, int worth)
        {
            Weight = weight;
            Worth = worth;
        }
    }
}
