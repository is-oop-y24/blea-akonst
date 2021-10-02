namespace Shops.Services
{
    public interface IShopsService
    {
        Shop AddShop(string shopName);
        void AddGoodsToShop(Good good, uint shopId);
        Good BuyGoodsInShop(uint shopId, string goodName, uint goodCount, Person person);
        Shop BuyTheCheapestGoods(string goodName, uint goodCount, Person person);
        void SetGoodPrice(uint shopId, string goodName, uint goodPrice);
    }
}