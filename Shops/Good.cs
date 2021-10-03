namespace Shops
{
    public class Good
    {
        public Good(string name, uint count, uint price)
        {
            Name = name;
            Count = count;
            Price = price;
        }

        public string Name { get; }
        public uint Count { get; set; }
        public uint Price { get; set; }
    }
}