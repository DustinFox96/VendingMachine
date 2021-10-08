using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine tester1 = new VendingMachine();
            tester1.StockInventory();
            tester1.FeedMoney(10);
            tester1.PurchaseItem("A1");
            
            tester1.GetChange();
        }
    }
}
