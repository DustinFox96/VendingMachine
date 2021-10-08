using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Menus
{
    public class MainMenu : NewMenu

    {

        public override List<string> MenuOptions { get; set; } = new List<string>()
        { "Display Vending Machine Items", "Purchase", "Exit"};

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
            Navigate(userSelection);
        }

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
                ChildMenus[0].Display();
            }
            else if (userSelection == 3)
            {
                Environment.Exit(0);
            }
            else
            {
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
