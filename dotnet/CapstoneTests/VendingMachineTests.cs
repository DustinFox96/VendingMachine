using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        [DataRow(5, "A1", 1.95)]
        public void GetChangeTest(double feed, string purchaseItem, double expected)
        {
            //arrange
            VendingMachine vendingMachine = new VendingMachine();

            //act
            vendingMachine.StockInventory();
            vendingMachine.FeedMoney((decimal)feed);
            vendingMachine.PurchaseItem(purchaseItem);
            decimal actual = vendingMachine.GetChange();

            //assert
            Assert.AreEqual((decimal)expected, actual);
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
    }
}
