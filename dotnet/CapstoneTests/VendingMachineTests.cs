using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        [DataRow(5, 5)]
        [DataRow(0, 0)]
        [DataRow(6, 0)]
        public void FeedMoneyTest(int moneyEntered, int expected)
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();

            //act
            vendingMachine.FeedMoney(moneyEntered);
            decimal actual = vendingMachine.CurrentBalance;

            //assert
            Assert.AreEqual(expected, actual);
        }

        //TODO: is there a better way to use decimals in the datarows? instead of casting them inside the body of the test
        [TestMethod]
        [DataRow(5, "C4")]
        public void GetChangeTest(double feed, string purchaseItem)
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();
            Dictionary<string, int> fakeDictionary = new Dictionary<string, int>()
            {
                {"Quarters", 14 }, {"Dimes", 0 }, {"Nickels", 0 }, {"Pennies", 0 }
            };

            Dictionary<string, int> expected = fakeDictionary;

            //act
            vendingMachine.StockInventory();
            vendingMachine.FeedMoney((decimal)feed);
            vendingMachine.PurchaseItem(purchaseItem);
            Dictionary<string, int> actual = vendingMachine.GetChange();

            //assert
           CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("B3", "Out of Stock")]
        public void OutOfStockTest(string purchaseItem, string expected)
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();

            //act
            vendingMachine.StockInventory();
            vendingMachine.FeedMoney(10);

            //attempt to purchase the item six times. we have an insatiable craving for chips
            vendingMachine.PurchaseItem(purchaseItem);
            vendingMachine.PurchaseItem(purchaseItem);
            vendingMachine.PurchaseItem(purchaseItem);
            vendingMachine.PurchaseItem(purchaseItem);
            vendingMachine.PurchaseItem(purchaseItem);

            //#6; the one that should fail
            string actual = vendingMachine.PurchaseItem(purchaseItem);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("A1", "Successfully purchased Potato Crisps")]

        public void CorrectTransactionTest(string itemToPurchase, string actual)
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();

            //act
            vendingMachine.StockInventory();
            vendingMachine.FeedMoney(10);
            string expected = vendingMachine.PurchaseItem(itemToPurchase);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
