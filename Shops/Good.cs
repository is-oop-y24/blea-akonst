namespace Shops
{
    public class Good
    {
        public Good(string goodName, uint goodCount, uint goodPrice)
        {
            Name = goodName;
            Count = goodCount;
            Price = goodPrice;
        }

        public string Name { get; }
        public uint Count { get; set; }
        public uint Price { get; set; }
    }
}