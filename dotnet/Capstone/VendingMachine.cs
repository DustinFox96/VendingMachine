using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Capstone
{
    public class VendingMachine
    {
        public Dictionary<string, ProductItem> ItemInventory { get; set; } = new Dictionary<string, ProductItem>();
        public decimal CurrentBalance { get; set; }
        

        //Method
        public void FeedMoney(decimal moneyPutIn)
        {
            if (moneyPutIn < 0)
            {
                Console.WriteLine("Nice try pal");
                return;
            }
            CurrentBalance += moneyPutIn;
            AddLog("Feed Money", moneyPutIn);
        }

        //Method
        //This will returning the remaining balance to the user whenever the user exits the pruchase menu.
        public decimal GetChange()
        {
            decimal change = CurrentBalance;
            CurrentBalance = 0;
            AddLog("GIVE CHANGE", change);
            return change;
        }

        //Method
        //Checks to make sure item exist, is in stock, and if current balance meets price. if so, proceed with the transaction.
        public void PurchaseItem(string ID)
        {
            if (!ItemInventory.ContainsKey(ID))
            {
                Console.WriteLine("Try picking something we have, open up your eyes");
                return;
            }

            if (ItemInventory[ID].ProductStock < 1)
            {
                Console.WriteLine("OUT OF STOCK");
                return;
            }

            if (ItemInventory[ID].ProductPrice > CurrentBalance)
            {
                Console.WriteLine("Poor people don't eat");
                return;
            }

            AddLog(ItemInventory[ID]);
            CurrentBalance -= ItemInventory[ID].ProductPrice;
            ItemInventory[ID].ProductStock--;
            Console.WriteLine(ItemInventory[ID].MakeSound());
            ItemInventory[ID].MakeSound();
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
                        string[] currentProductItemInfo = new string[4];
                        currentProductItemInfo = currentLine.Split("|");


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
