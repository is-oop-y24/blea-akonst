namespace Shops.Services
{
    public interface IShopsService
    {
        public Shop AddShop(string shopName);
        public Good AddGoodsToShop(uint shopId, string goodName, uint goodCount, uint goodPrice);
        public Good BuyGoodsInShop(uint shopId, string goodName, uint goodCount, Person person);
        public Good BuyTheCheapestGoods(string goodName, uint goodCount, Person person);
        public void SetGoodPrice(uint shopId, string goodName, uint goodPrice);
    }
}