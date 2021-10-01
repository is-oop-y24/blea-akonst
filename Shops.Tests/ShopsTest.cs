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
            // creating the person with 30000 bucks
            uint startBalance = 30000;
            var person = new Person(startBalance);
            
            // creating the shop
            Shop shop = _shopsService.AddShop("Amazon");

            // adding something to shop
            uint packageCount = 5;
            Good iphone = _shopsService.AddGoodsToShop(shop.Id, "Apple iPhone 13", packageCount, 999);
            Good samsung = _shopsService.AddGoodsToShop(shop.Id, "Samsung Galaxy S228", packageCount, 666);
            
            // buying something
            uint requiredIphones = 3;
            uint requiredSamsungs = 2;
            _shopsService.BuyGoodsInShop(shop.Id, "Apple iPhone 13", requiredIphones, person);
            _shopsService.BuyGoodsInShop(shop.Id, "Samsung Galaxy S228", requiredSamsungs, person);

            // money has been sent to the shop and count of goods has been changed
            Assert.AreEqual(startBalance - iphone.Price * requiredIphones - samsung.Price * requiredSamsungs, person.Balance);
            Assert.AreEqual(packageCount - requiredIphones, iphone.Count);
            Assert.AreEqual(packageCount - requiredSamsungs, samsung.Count);
        }

        [Test]
        public void ChangingPriceOfGoods()
        {
            // creating the shop
            Shop shop = _shopsService.AddShop("Ozon");
            
            // adding something to shop
            uint startPrice = 2;
            string goodName = "Milk";
            Good milk = _shopsService.AddGoodsToShop(shop.Id, goodName, 1, startPrice);
            
            // changing price of good
            uint newPrice = 100500; // inflation in Russian 90's
            _shopsService.SetGoodPrice(shop.Id, goodName, newPrice);
            
            // check that new price is valid
            Assert.AreEqual(newPrice, milk.Price);
        }

        [Test]
        public void FindShopWithCheapestGood()
        {
            // creating shops and person
            var person = new Person(45000);
            Shop shop1 = _shopsService.AddShop("Pyaterochka");
            Shop shop2 = _shopsService.AddShop("Magnit");
            Shop shop3 = _shopsService.AddShop("Dixy");

            // adding same good with different prices to shops
            string goodName = "Snickers";
            uint cheapestPrice = 1;
            _shopsService.AddGoodsToShop(shop1.Id, goodName, 100, 2);
            _shopsService.AddGoodsToShop(shop2.Id, goodName, 100, cheapestPrice);
            _shopsService.AddGoodsToShop(shop3.Id, goodName, 100, 16);
            
            // trying to find
            Good resultGood = _shopsService.BuyTheCheapestGoods(goodName, 70, person);
            Assert.AreEqual(resultGood.Price, cheapestPrice);
        }

        [Test]
        public void PurchaseOfGoodsInLargerQuantitiesThanInStock_ThrowException()
        {
            // creating a shop and a person
            Shop shop1 = _shopsService.AddShop("Pyaterochka");
            var person = new Person(40000);
            
            // adding goods
            _shopsService.AddGoodsToShop(shop1.Id, "Water 5l", 5, 100);
            
            Assert.Catch<ShopsException>(() =>
            {
                _shopsService.BuyGoodsInShop(shop1.Id, "Water 5l", 6, person);
            });
        }
    }
}