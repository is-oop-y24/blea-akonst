using System;
using System.Collections.Generic;
using Shops.Services;
using Shops.Tools;

namespace Shops
{
    public class ShopsService : IShopsService
    {
        private static uint _shopId = 1;
        private Dictionary<uint, Shop> _shopList = new Dictionary<uint, Shop>();

        public Shop AddShop(string shopName) => _shopList[_shopId] = new Shop(shopName, _shopId++);

        public Good AddGoodsToShop(uint shopId, string goodName, uint goodCount, uint goodPrice)
        {
            return _shopList[shopId].AddGood(goodName, goodPrice, goodCount);
        }

        public Good BuyGoodsInShop(uint shopId, string goodName, uint goodCount, Person person)
        {
            try
            {
                Good good = _shopList[shopId].TakeGood(goodName, goodCount);
                person.Balance -= good.Price * goodCount;
                return good;
            }
            catch (ShopsException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Good BuyTheCheapestGoods(string goodName, uint goodCount, Person person)
        {
            uint shopWithTheCheapestGoods = 0;
            uint currentLowestPrice = uint.MaxValue;

            foreach (var shop in _shopList)
            {
                try
                {
                    uint currentPrice = shop.Value.GetGoodPrice(goodName);

                    if (currentPrice >= currentLowestPrice) continue;

                    currentLowestPrice = currentPrice;
                    shopWithTheCheapestGoods = shop.Key;
                }
                catch (ShopsException)
                {
                }
            }

            if (shopWithTheCheapestGoods.Equals(0))
            {
                throw new ShopsException("There are no shops with this good in this count!");
            }

            return BuyGoodsInShop(shopWithTheCheapestGoods, goodName, goodCount, person);
        }

        public void SetGoodPrice(uint shopId, string goodName, uint goodPrice)
        {
            _shopList[shopId].ChangeGoodPrice(goodName, goodPrice);
        }
    }
}