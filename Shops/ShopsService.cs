using System.Collections.Generic;
using System.Linq;
using Shops.Services;
using Shops.Tools;

namespace Shops
{
    public class ShopsService : IShopsService
    {
        private static uint _shopId = 1;
        private List<Shop> _shops = new List<Shop>();

        public Shop AddShop(string shopName)
        {
            var shop = new Shop(shopName, _shopId++);
            _shops.Add(shop);
            return shop;
        }

        public void AddGoodsToShop(Good good, uint shopId)
        {
            Shop shop = _shops.FirstOrDefault(sh => sh.Id.Equals(shopId));
            if (shop == default(Shop))
            {
                throw new ShopsException("This shop doesn't exists!");
            }

            shop.AddGood(good);
        }

        public Good BuyGoodsInShop(uint shopId, string goodName, uint goodCount, Person person)
        {
            Shop shop = _shops.FirstOrDefault(sh => sh.Id.Equals(shopId));
            if (shop == default(Shop))
            {
                throw new ShopsException("This shop doesn't exists!");
            }

            Good good = shop.TakeGood(goodName, goodCount);

            if (person.Balance < good.Price * goodCount)
            {
                throw new ShopsException("Person doesn't have enough money for this transaction!");
            }

            person.Balance -= goodCount * good.Price;

            return good;
        }

        public Shop BuyTheCheapestGoods(string goodName, uint goodCount, Person person)
        {
            var shopWithTheCheapestGoods = default(Shop);
            uint currentLowestPrice = uint.MaxValue;

            foreach (Shop shop in _shops)
            {
                uint currentPrice = shop.GetGoodPrice(goodName);

                if (currentPrice >= currentLowestPrice) continue;
                currentLowestPrice = currentPrice;
                shopWithTheCheapestGoods = shop;
            }

            if (shopWithTheCheapestGoods == default(Shop))
            {
                throw new ShopsException("There are no shops with this good in this count!");
            }

            return shopWithTheCheapestGoods;
        }

        public void SetGoodPrice(uint shopId, string goodName, uint goodPrice)
        {
            Shop shop = _shops.FirstOrDefault(sh => sh.Id.Equals(shopId));
            if (shop == default(Shop))
            {
                throw new ShopsException("This shop doesn't exists!");
            }

            shop.ChangeGoodPrice(goodName, goodPrice);
        }
    }
}