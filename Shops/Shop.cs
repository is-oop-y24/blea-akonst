using System.Collections.Generic;
using Shops.Tools;

namespace Shops
{
    public class Shop
    {
        private Dictionary<string, Good> _goodsList = new Dictionary<string, Good>();

        public Shop(string shopName, uint shopId)
        {
            Name = shopName;
            Id = shopId;
        }

        public string Name { get; }
        public uint Id { get; }

        public void ChangeGoodPrice(string name, uint price) => _goodsList[name].Price = price;

        public uint GetGoodPrice(string name)
        {
            if (_goodsList.ContainsKey(name))
            {
                return _goodsList[name].Price;
            }
            else
            {
                throw new ShopsException("This good doesn't exists!");
            }
        }

        public uint GetGoodCount(string name)
        {
            if (_goodsList.ContainsKey(name))
            {
                return _goodsList[name].Count;
            }
            else
            {
                throw new ShopsException("This good doesn't exists!");
            }
        }

        public Good AddGood(string name, uint price, uint count)
        {
            if (_goodsList.ContainsKey(name))
            {
                _goodsList[name].Count += count;
                _goodsList[name].Price = price;
            }
            else
            {
                _goodsList[name] = new Good(name, count, price);
            }

            return _goodsList[name];
        }

        public Good TakeGood(string name, uint count)
        {
            if (_goodsList.ContainsKey(name))
            {
                Good good = _goodsList[name];
                if (good.Count >= count)
                {
                    good.Count -= count;
                    return good;
                }
                else
                {
                    throw new ShopsException("This count of good is not exists in this shop!");
                }
            }
            else
            {
                throw new ShopsException("This good doesn't exists in this shop!");
            }
        }
    }
}