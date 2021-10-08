using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Menu
    {
        public List<string> MenuOptions { get; }
        public VendingMachine CurrentVendingMachine { get; }
        public Menu ParentMenu { get; }

        //Method
        public void DisplayMainMenu()
        {
            // Loop through the menu, writing each option to the console next to its numbered 'place' in the menu
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {MenuOptions[i]}");
            }
            Console.WriteLine();
            Console.WriteLine("Please select an option (1-3)");
            int userSelection = 0;
            try
            {
                userSelection = int.Parse(Console.ReadLine());

            }
            catch (Exception)
            {

                Console.WriteLine($"Not a option, please read better");
                Console.WriteLine();
                Console.WriteLine("Press the any key to proceed back to the Display Menu");
                Console.ReadKey();
            }

            Console.Clear();
            NavigateMainMenu(userSelection);
        }

        //Method
        public void DisplayPurchaseMenu()
        {
            // Loop through the menu, writing each option to the console next to its numbered 'place' in the menu

            //CurrentVendingMachine.DisplayInventory();

            Console.WriteLine();
            Console.WriteLine($"Current balance: ${CurrentVendingMachine.CurrentBalance}");
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {MenuOptions[i]}");
            }
            Console.WriteLine("Please select an option (1-3)");
            int userSelection = 0;
            try
            {
                userSelection = int.Parse(Console.ReadLine());

            }
            catch (Exception)
            {

                Console.WriteLine($"Not a option, please read better");
                Console.WriteLine();
                Console.WriteLine("Press the any key to proceed back to the Purchase Menu");
                Console.ReadKey();
            }
            Console.Clear();
            NavigatePurchaseMenu(userSelection);
        }

        //Method
        public void NavigateMainMenu(int userSelection)
        {
            if (userSelection == 1)
            {
                CurrentVendingMachine.DisplayInventory();
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

                Menu menu = new Menu(purchaseMenu, CurrentVendingMachine, this);
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
            else
            {
                DisplayMainMenu();
            }


        }

        //Method
        public void NavigatePurchaseMenu(int userSelection)
        {
            //Feed Money
            if (userSelection == 1)
            {
                Console.WriteLine("Please insert dollar bill yo");
                decimal enteredMoney = decimal.Parse(Console.ReadLine());
                CurrentVendingMachine.FeedMoney(enteredMoney);
                Console.WriteLine("Press the any key to proceed back to the purchase menu");

                Console.Clear();
                this.DisplayPurchaseMenu();

            }

            // Select Product
            else if (userSelection == 2)
            {
                foreach (KeyValuePair<string, ProductItem> item in CurrentVendingMachine.ItemInventory)
                {
                    Console.WriteLine($"{item.Key} - {item.Value.Name} - {item.Value.ProductPrice}");

                }
                Console.WriteLine();
                Console.WriteLine("Please select snack ID (like A1):");
                string selectedProduct = Console.ReadLine();
                CurrentVendingMachine.PurchaseItem(selectedProduct);
                Console.ReadKey();
                Console.Clear();
                this.DisplayPurchaseMenu();

            }

            // Finish Transaction
            else if (userSelection == 3)
            {
                Dictionary<string, int> changeDictionary = CurrentVendingMachine.GetChange();
                foreach (KeyValuePair<string, int> key in changeDictionary)
                {
                    if (key.Value > 0)
                    {
                        Console.Write($" Cha-ching, you get {key.Value} {key.Key}");
                    }
                }
                Console.ReadKey();
                Console.Clear();
                ParentMenu.DisplayMainMenu();
            }
            else
            {
                ParentMenu.DisplayPurchaseMenu();
            }

        }

        //CTOR
        public Menu(List<String> menuOptions, VendingMachine currentVendingMachine, Menu parentMenu)
        {
            MenuOptions = menuOptions;
            CurrentVendingMachine = currentVendingMachine;
            ParentMenu = parentMenu;

        }
    }
    //Danger Zone
}
