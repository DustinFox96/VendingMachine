using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Menus
{
    public class PurchaseMenu : NewMenu
    {
        public override List<string> MenuOptions { get; set; } = new List<string>()
        { "Feed Money","Select Product", "Finish Transaction" };

        //Method
        public override void Navigate(int userSelection)
        {
            //Feed Money
            if (userSelection == 1)
            {
                Console.WriteLine("Please insert dollar bill yo");
                try
                {
                decimal enteredMoney = decimal.Parse(Console.ReadLine());
                CurrentVendingMachine.FeedMoney(enteredMoney);
                Console.WriteLine("Press the any key to proceed back to the purchase menu");
                }
                catch (Exception)
                {
                    
                    Console.WriteLine("Pretty sure that is not money");
                    Console.ReadKey();
                }

                Console.Clear();
                Display();

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
                Display();

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
                ParentMenu.Display();
            }
            else
            {
                ParentMenu.Display();
            }

        }


        //Method
        //Displays the purchase menu options and saves users input.
        public override void Display()
        {
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
            Navigate(userSelection);
        }



        //CTOR
        //Inherited from parent class
        public PurchaseMenu(string name, VendingMachine currentVendingMachine) : base(name, currentVendingMachine)
        {
        }
    }
    //DANGER ZONE
}
