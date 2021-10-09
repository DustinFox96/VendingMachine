using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Capstone
{
    public class VendingMachine
    {
        public Dictionary<string, ProductItem> ItemInventory { get; private set; } = new Dictionary<string, ProductItem>();
        public decimal CurrentBalance { get; private set; }


        //Method
        public void FeedMoney(decimal moneyPutIn)
        {
            // If the user has somehow entered less than one cent,
            // chastise them for attempting to pull a fast one on us.
            if (moneyPutIn < 0)
            {
                Console.WriteLine("Nice try pal");
                // Console.ReadKey();
                return;
            }
            // We only accept valid tender
            if (moneyPutIn == 1.00m || moneyPutIn == 2.00m || moneyPutIn == 5.00m || moneyPutIn == 10.00m)
            {
                CurrentBalance += moneyPutIn;
                AddLog("Feed Money", moneyPutIn);
            }
            else
            {
                Console.Write("Go take your Monopoly money elsewhere! (We only accept 1s, 2s, 5s, and 10 bills)");
                Console.ReadKey();
                return;
            }
        }

        //Method
        //This will returning the remaining balance to the user whenever the user exits the purchase menu.
        //Returns value in change such as dimes and nickels
        public Dictionary<string, int> GetChange()
        {
            

            Dictionary<string, int> changeDictionary = new Dictionary<string, int>()
            {
                {"Quarters", 0 }, {"Dimes", 0 }, {"Nickels", 0 }, {"Pennies", 0 }
            };

            decimal change = CurrentBalance;

            while (CurrentBalance >= .25m)
            {
                CurrentBalance -= .25m;
                changeDictionary["Quarters"]++;
            }
            while (CurrentBalance >= .10m)
            {
                CurrentBalance -= .10m;
                changeDictionary["Dimes"]++;
            }
            while (CurrentBalance >= .05m)
            {
                CurrentBalance -= .05m;
                changeDictionary["Nickels"]++;
            }
            while (CurrentBalance >= .01m)
            {
                CurrentBalance -= .01m;
                changeDictionary["Penny"]++;
            }

            AddLog("GIVE CHANGE", change);
            return changeDictionary;
        }

        //Method
        //Checks to make sure item exist, is in stock, and if current balance meets price. if so, proceed with the transaction.
        public string PurchaseItem(string ID)
        {

            if (!ItemInventory.ContainsKey(ID))
            {
                string message = "Try picking something we have, open up your eyes";
                Console.Write(message);
                //Console.ReadKey();
                return message;
            }

            //creating a easiser variable to call on when we need to use current items ID
            ProductItem item = ItemInventory[ID];

            if (item.ProductStock < 1)
            {
                string message = "Out of Stock";
                Console.Write(message);
                return message;
            }

            if (item.ProductPrice > CurrentBalance)
            {
                string message = "Poor people don't eat";
                Console.WriteLine(message);
                Console.WriteLine();
                Console.WriteLine("Press the any key to return to the purchase menu");
                return message;
            }

            AddLog(item);
            CurrentBalance -= item.ProductPrice;
            item.ProductStock--;
            Console.WriteLine($"\nThat {item.Name} just ran you {item.ProductPrice} and now you have {CurrentBalance} current balance remaining");
            Console.WriteLine(item.MakeSound());

            //this adds +1 to our sales report for this item
            item.ItemSalesReport++;

            return $"Successfully purchased {item.Name}";
            
        }

        //Method
        //Taking a log of both of what the user gives us in money and what we give back in change
        public void AddLog(string activity, decimal usersMoney)
        {
            string currentPath = Directory.GetCurrentDirectory();
            //combines our current path to the targetfile we will be writing on.
            string targetFile = Path.Combine(currentPath, "log.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(targetFile, true))
                {
                    sw.WriteLine($"{DateTime.Now} {activity} ${usersMoney} ${CurrentBalance} ");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        //Method
        //This is a overloaded verison on the method up above.
        //creating a log based off of product purchase. will take currnet balance and subtract the items value.
        public void AddLog(ProductItem productName)
        {
            string currentPath = Directory.GetCurrentDirectory();
            //combines our current path to the targetfile we will be writing on.
            string targetFile = Path.Combine(currentPath, "log.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(targetFile, true))
                {
                    sw.WriteLine($"{DateTime.Now} {productName.Name} ${CurrentBalance} ${CurrentBalance - productName.ProductPrice}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        //Method
        //Reads off the CSV file and delimits it and creates a string array to store it's info
        //we use the array index to set eachs product Id, Name, Price and what snack Type it is.
        public void StockInventory()

        {
            string currentLine = "";
            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "vendingmachine.csv")))
                {
                    while (!sr.EndOfStream)
                    {
                        currentLine = sr.ReadLine();
                        string[] currentProductItemInfo = currentLine.Split("|");

                        string currentId = currentProductItemInfo[0];
                        string currentName = currentProductItemInfo[1];
                        decimal currentPrice = decimal.Parse(currentProductItemInfo[2]);
                        string currentType = currentProductItemInfo[3];

                        ProductItem currentItem = new ProductItem(currentName, currentPrice, currentType);
                        ItemInventory.Add(currentId, currentItem);
                    }
                }
            }

            catch (Exception theseHands)
            {
                Console.WriteLine(theseHands.Message);
            }


        }

        //Method
        //Used to call on whenever we want to display our products.
        //When product stock count reaches 0, it displays it's name and indicates it is sold out.
        public void DisplayInventory()
        {
            foreach (KeyValuePair<string, ProductItem> item in ItemInventory)
            {
                if (item.Value.ProductStock < 1)
                {
                    Console.WriteLine($"{item.Key}: {item.Value.Name}: SOLD OUT");
                }
                else
                {
                    Console.WriteLine($"{item.Key}: {item.Value.Name}: {item.Value.ProductPrice}");
                }
            }
        }

    }
    //DANGER ZONE
}
