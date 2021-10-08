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
                return;
            }
        }

        //Method
        //This will returning the remaining balance to the user whenever the user exits the purchase menu.
        public decimal GetChange()
        {
            decimal change = CurrentBalance;
            CurrentBalance = 0;
            AddLog("GIVE CHANGE", change);
            return change;
        }

        //Method
        //Checks to make sure item exist, is in stock, and if current balance meets price. if so, proceed with the transaction.
        public string PurchaseItem(string ID)
        {
            if (!ItemInventory.ContainsKey(ID))
            {
                string message = "Try picking something we have, open up your eyes";
                Console.Write(message);
                return message;
            }

            if (ItemInventory[ID].ProductStock < 1)
            {
                string message = "Out of Stock";
                Console.Write(message);
                return message;
            }

            if (ItemInventory[ID].ProductPrice > CurrentBalance)
            {
                string message = "Poor people don't eat";
                Console.Write(message);
                return message;
            }

            AddLog(ItemInventory[ID]);
            CurrentBalance -= ItemInventory[ID].ProductPrice;
            ItemInventory[ID].ProductStock--;

            Console.WriteLine(ItemInventory[ID].MakeSound());
            return "Purchase successful!";


            //bounce back to menu
            //take log of what happened here
        }

        //Method
        //Taking a log of of both of what the user gives us in money and what we give back in change
        public void AddLog(string activity, decimal usersMoney)
        {
            string currentPath = Directory.GetCurrentDirectory();
            //combines our current path to the targetfile we will be writing on.
            string targetFile = Path.Combine(currentPath, "log.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(targetFile, true))
                {
                    sw.WriteLine($"{DateTime.Now} {activity} {usersMoney} {CurrentBalance} ");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        //Method
        //creating a log based off of prudct purchase. will take currnet balance and subtract the items value.
        public void AddLog(ProductItem productName)
        {
            string currentPath = Directory.GetCurrentDirectory();
            //combines our current path to the targetfile we will be writing on.
            string targetFile = Path.Combine(currentPath, "log.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(targetFile, true))
                {
                    sw.WriteLine($"{DateTime.Now} {productName.Name} {CurrentBalance} {CurrentBalance - productName.ProductPrice}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        //Method
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

        //DANGER ZONE
    }
}
