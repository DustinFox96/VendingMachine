using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public abstract class NewMenu
    {
        public string Name { get; set; }
        public abstract List<string> MenuOptions { get; set; }
        public NewMenu ParentMenu { get; set; }
        public List<NewMenu> ChildMenus { get; set; } = new List<NewMenu>();
        public VendingMachine CurrentVendingMachine { get; }

        //Method
        //child class will be expected to insert their own menuOption display here
        public virtual void Display() { }

        //Method
        //child class will be expected to record users input
        public virtual void Navigate(int selectedOption) { }

        //CTOR
        public NewMenu(string name, VendingMachine currentVendingMachine)
        {
            Name = name;
            
            CurrentVendingMachine = currentVendingMachine;
        }

        //CTOR
        //This is a overloaded CTOR
        public NewMenu(string name, List<string> menuOptions, NewMenu parentMenu, List<NewMenu> childMenus)
        {
            Name = name;
            MenuOptions = menuOptions;
            ParentMenu = parentMenu;
            ChildMenus = childMenus;
        }
    }
    //DANGER ZONE
}
