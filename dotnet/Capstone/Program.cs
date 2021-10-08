using Capstone.Menus;
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



            NewMenu mainMenu = new MainMenu("Main Menu", tester1);

            NewMenu purchaseMenu = new PurchaseMenu("Purchase Mene", tester1);

            mainMenu.ChildMenus.Add(purchaseMenu);
            purchaseMenu.ParentMenu = mainMenu;

            mainMenu.Display();

            //Menu menu = new Menu(mainMenu, tester1, null);
            //menu.DisplayMainMenu();
        }
        //DANGER ZONE
    }
}
