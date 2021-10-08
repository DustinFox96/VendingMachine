using System;
using System.Collections.Generic;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine tester1 = new VendingMachine();
            tester1.StockInventory();

            List<string> mainMenu = new List<string>()
            {
                "Display Vending Machine Items",
                "Purchase",
                "Exit"
            };

            Menu menu = new Menu(mainMenu, tester1, null);
            menu.DisplayMainMenu();

            

   
            //tester1.FeedMoney(10);
            //tester1.PurchaseItem("A1");
            
            //tester1.GetChange();
        }
    }
}
