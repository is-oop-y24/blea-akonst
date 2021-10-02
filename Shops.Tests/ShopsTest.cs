using NUnit.Framework;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    [TestFixture]
    public class Tests
    {
        private IShopsService _shopsService;
        
        [SetUp]
        public void Setup()
        {
            _shopsService = new ShopsService();
        }

        [Test]
        public void DeliveryOfGoodsToTheStore()
        {
            uint startBalance = 30000;
            var person = new Person(startBalance);
            
            Shop shop = _shopsService.AddShop("Amazon");
            
            uint packageCount = 5;
            var iphone = new Good("iPhone", packageCount, 999);
            var samsung = new Good("Samsung", packageCount, 666);
            _shopsService.AddGoodsToShop(iphone, shop.Id);
            _shopsService.AddGoodsToShop(samsung, shop.Id);
            
            uint requiredIphones = 3;
            uint requiredSamsungs = 2;
            _shopsService.BuyGoodsInShop(shop.Id, "iPhone", requiredIphones, person);
            _shopsService.BuyGoodsInShop(shop.Id, "Samsung", requiredSamsungs, person);

            // money has been sent to the shop and count of goods has been changed
            Assert.AreEqual(startBalance - iphone.Price * requiredIphones - samsung.Price * requiredSamsungs, person.Balance);
            Assert.AreEqual(packageCount - requiredIphones, shop.GetGoodCount("iPhone"));
            Assert.AreEqual(packageCount - requiredSamsungs, shop.GetGoodCount("Samsung"));
        }

        [Test]
        public void ChangingPriceOfGoods()
        {
            Shop shop = _shopsService.AddShop("Ozon");
            
            var milk = new Good("Milk", 1, 2);
            _shopsService.AddGoodsToShop(milk, shop.Id);
            
            uint newPrice = 100500; 
            _shopsService.SetGoodPrice(shop.Id, milk.Name, newPrice);
            
            // check that new price is valid
            Assert.AreEqual(newPrice, shop.GetGoodPrice("Milk"));
        }

        [Test]
        public void FindShopWithCheapestGood()
        {
            var person = new Person(45000);
            Shop shop1 = _shopsService.AddShop("Pyaterochka");
            Shop shop2 = _shopsService.AddShop("Magnit");
            Shop shop3 = _shopsService.AddShop("Dixy");
            
            string goodName = "Snickers";
            uint cheapestPrice = 10;
            var snickers = new Good(goodName, 100, 150);
            _shopsService.AddGoodsToShop(snickers, shop1.Id);
            _shopsService.AddGoodsToShop(snickers, shop2.Id);
            _shopsService.AddGoodsToShop(snickers, shop3.Id);
            _shopsService.SetGoodPrice(shop2.Id, goodName, cheapestPrice);
            _shopsService.SetGoodPrice(shop3.Id, goodName, 30);
            
            
            // trying to find
            Shop cheapestShop = _shopsService.BuyTheCheapestGoods("Snickers", 50, person);
            Assert.AreEqual(cheapestShop.GetGoodPrice(goodName), cheapestPrice);
        }

        [Test]
        public void PurchaseOfGoodsInLargerQuantitiesThanInStock_ThrowException()
        {
            Shop shop1 = _shopsService.AddShop("Pyaterochka");
            var person = new Person(40000);
            
            var good = new Good("Water", 5, 100); 
            _shopsService.AddGoodsToShop(good, shop1.Id);
            
            Assert.Catch<ShopsException>(() =>
            {
                _shopsService.BuyGoodsInShop(shop1.Id, "Water 5l", 6, person);
            });
        }
    }
}