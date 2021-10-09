using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Menus
{
    public class MainMenu : NewMenu

    {

        public override List<string> MenuOptions { get; set; } = new List<string>()
        { "Display Vending Machine Items", "Purchase", "Exit"};

        //Method
        //This is displaying and listing our main menus options
        public override void Display()
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
            //Invoke the navigate method on the option provoided by the user
            Navigate(userSelection);
        }

        //Method
        //defines what each option does
        public override void Navigate(int userSelection)
        {
            if (userSelection == 1)
            {
                CurrentVendingMachine.DisplayInventory();
                Console.WriteLine();
                Display();

            }
            else if (userSelection == 2)
            {
                //call the display method of our first child menus
                ChildMenus[0].Display();
            }
            else if (userSelection == 3)
            {
                //quits the program.. not quite sure what this does.. ask mike
                Environment.Exit(0);
            }
            //This displays our secert sales report menu and let's us know how much has been sold per item and what our total
            //revenue is.
            else if (userSelection == 12)
            {
                decimal totalSum = 0;
                Console.WriteLine($"SALES REPORT FOR {DateTime.Now}");
                Console.WriteLine();
                foreach (KeyValuePair<string, ProductItem> item in CurrentVendingMachine.ItemInventory)
                {
                    Console.WriteLine($"{item.Value.Name} {item.Value.ItemSalesReport}");
                    totalSum += (item.Value.ItemSalesReport * item.Value.ProductPrice);
                }
                Console.WriteLine();
                Console.WriteLine($"Tech Elevator is now ${totalSum} richer.");
            }
            else
            {
                //kicks us back to the main menu is our choices don't match was provoided
                Console.WriteLine($"{userSelection} is not a option");
                Console.WriteLine();
                Display();
            }


        }

        //CTOR
        //Inherited from parent class
        public MainMenu(string name, VendingMachine currentVendingMachine) : base(name, currentVendingMachine)
        {
        }
    }
    //DANGER ZONE
}
