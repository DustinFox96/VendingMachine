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


            //creating a new object of NewMenu Class here. Have to give it a object of vendingmachine and name it as well
            NewMenu mainMenu = new MainMenu("Main Menu", tester1);

            //creating a new object of NewMenu Class here. Have to give it a object of vendingmachine and name it as well
            NewMenu purchaseMenu = new PurchaseMenu("Purchase Menu", tester1);

            //add the purchase menu as a child to mainMenu
            mainMenu.ChildMenus.Add(purchaseMenu);
            purchaseMenu.ParentMenu = mainMenu;

            mainMenu.Display();

            //Menu menu = new Menu(mainMenu, tester1, null);
            //menu.DisplayMainMenu();
        }
        //DANGER ZONE
    }
}
