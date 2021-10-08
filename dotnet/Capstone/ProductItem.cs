using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ProductItem
    {
        public string Name { get; set; }
        public string ProductType { get; set; }
        public decimal ProductPrice { get; set; }
        //Making the default stock of 5, that way when the app is closed and open, it's always given the stock of 5.
        public int ProductStock { get; set; } = 5;

        //Method
        //The sound that should be displayed to the console depending what the product type is
        public virtual string MakeSound()
        {
            if (ProductType == "Chip") return "Crunch Crunch, Yummy!";
            if (ProductType == "Candy") return "Munch Munch, Yummy!";
            if (ProductType == "Drink") return "Glug Glug, Yummy!";
            if (ProductType == "Gum") return "Chew Chew, Yummy!";

            //If the product type isn't one of the above, the message is generic:
            return "Yummy!";
        }
            
        //CTOR
        public ProductItem(string name, decimal productPrice, string productType)
        {
            Name = name;
            ProductPrice = productPrice;
            ProductType = productType;
        }
    }
    //DANGER ZONE
}
