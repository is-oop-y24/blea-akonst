using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops
{
    public class Shop
    {
        private List<Good> _goods = new List<Good>();

        public Shop(string shopName, uint shopId)
        {
            Name = shopName;
            Id = shopId;
        }

        public string Name { get; }
        public uint Id { get; }

        public void ChangeGoodPrice(string name, uint price)
        {
            Good good = _goods.FirstOrDefault(g => g.Name.Equals(name));
            if (good == default(Good))
            {
                throw new ShopsException("This good doesn't exists in this shop!");
            }

            good.Price = price;
        }

        public uint GetGoodPrice(string name)
        {
            Good good = _goods.FirstOrDefault(g => g.Name.Equals(name));
            if (good == default(Good))
            {
                throw new ShopsException("This good doesn't exists!");
            }

            return good.Price;
        }

        public uint GetGoodCount(string name)
        {
            Good good = _goods.FirstOrDefault(g => g.Name.Equals(name));
            if (good == default(Good))
            {
                throw new ShopsException("This good doesn't exists!");
            }

            return good.Count;
        }

        public Good AddGood(Good suppliedGood)
        {
            Good good = _goods.FirstOrDefault(g => g.Name.Equals(suppliedGood.Name));
            if (good != default(Good))
            {
                good.Count += suppliedGood.Count;
                good.Price = suppliedGood.Price;
            }
            else
            {
                _goods.Add(suppliedGood);
            }

            return _goods.First(g => g.Name.Equals(suppliedGood.Name));
        }

        public Good TakeGood(string name, uint count)
        {
            Good good = _goods.FirstOrDefault(g => g.Name.Equals(name));
            if (good == default(Good))
            {
                throw new ShopsException("This good doesn't exists in this shop!");
            }

            if (good.Count < count) throw new ShopsException("This count of good is not exists in this shop!");

            good.Count -= count;
            return good;
        }
    }
}