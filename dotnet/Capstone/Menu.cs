using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Menu
    {
        public List<string> MenuOptions { get; }
        public VendingMachine CurrentVendingMachine { get; }

        public void DisplayMainMenu()
        {
            // Loop through the menu, writing each option to the console next to its numbered 'place' in the menu
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {MenuOptions[i]}");
            }
            Console.WriteLine();
            Console.WriteLine("Please select an option (1-3)");
            int userSelection = int.Parse(Console.ReadLine());
            NavigateMainMenu(userSelection);
        }

        public void DisplayPurchaseMenu()
        {
            // Loop through the menu, writing each option to the console next to its numbered 'place' in the menu
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {MenuOptions[i]}");
            }
            Console.WriteLine();
            Console.WriteLine($"Current balance: ${CurrentVendingMachine.CurrentBalance}");
            Console.WriteLine("Please select an option (1-3)");
            int userSelection = int.Parse(Console.ReadLine());
            NavigatePurchaseMenu(userSelection);
        }

        public void NavigateMainMenu(int userSelection)
        {
            if (userSelection == 1)
            {
                foreach(KeyValuePair<string, ProductItem> item in CurrentVendingMachine.ItemInventory)
                {
                    Console.WriteLine($"{item.Key} - {item.Value.Name} - {item.Value.ProductPrice}");
                    
                }
                Console.WriteLine();
                DisplayMainMenu();

            }
            else if (userSelection == 2)
            {
                List<string> purchaseMenu = new List<string>()
            {
                "Feed Money",
                "Select Product",
                "Finish Transaction"
            };

                Menu menu = new Menu(purchaseMenu, CurrentVendingMachine);
                Console.WriteLine();
                menu.DisplayPurchaseMenu();

                //TODO?
                // create an interface that describes things you can put in the menu
                // that includes both items that 'do something' and other menus
                // that way you can nest menus inside menus
            }
            else if (userSelection == 3)
            {
                Environment.Exit(0);
            }


        }

        public void NavigatePurchaseMenu(int userSelection)
        {   //Feed Money
            if (userSelection == 1)
            {
                Console.WriteLine("Please insert dollar bill yo");
                decimal enteredMoney = decimal.Parse(Console.ReadLine());
                CurrentVendingMachine.FeedMoney(enteredMoney);
                this.DisplayPurchaseMenu();

            }
            // Select Product
            if (userSelection == 2)
            {
                foreach (KeyValuePair<string, ProductItem> item in CurrentVendingMachine.ItemInventory)
                {
                    Console.WriteLine($"{item.Key} - {item.Value.Name} - {item.Value.ProductPrice}");

                }
                Console.WriteLine();
                Console.WriteLine("Please select snack ID (like A1):");
                string selectedProduct = Console.ReadLine();
                CurrentVendingMachine.PurchaseItem(selectedProduct);
                this.DisplayPurchaseMenu();

            }
            // Finish Transaction
            if (userSelection == 3)
            {
                Console.WriteLine("Cha-ching! Your change is:");
                Console.WriteLine(CurrentVendingMachine.GetChange());
                DisplayMainMenu();
            }

        }


            public Menu(List<String> menuOptions, VendingMachine currentVendingMachine)
            {
                MenuOptions = menuOptions;
                CurrentVendingMachine = currentVendingMachine;

            }
        







        // string "1. Purchase Item" - List<>?
        // options OR another menu
    }
    //Danger Zone
}
