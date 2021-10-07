using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine tester1 = new VendingMachine();
            tester1.StockInventory();
            tester1.FeedMoney(-1.00m);
            tester1.PurchaseItem("D4");
            
            tester1.GetChange();
        }
    }
}
