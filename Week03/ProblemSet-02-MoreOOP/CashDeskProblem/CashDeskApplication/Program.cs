using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashDesk;

namespace CashDeskApplication
{
    class Program
    {
        static void TakeBatchOfCoins(string[] words, CashDesk.CashDesk desk)
        {
            Coin[] amounts = new Coin[words.Length - 2];
            bool validInput = true;
            int total = 0;
            for (int i = 0; i < words.Length - 2; i++)
            {
                int curAmount;
                if (int.TryParse(words[i + 2], out curAmount))
                {
                    amounts[i] = new Coin(curAmount);
                    total += curAmount;
                }
                else
                {
                    validInput = false;
                    break;
                }
            }
            if (validInput)
            {
                List<int> rejected;
                desk.TakeMoney(new BatchCoin(amounts), out rejected);
                if (rejected.Count == 0) Console.WriteLine("Successfully taken {0} coins with total amount of {1}¢!", amounts.Length, total);
                else
                {
                    Console.Write("Invalid coins were rejected: ");
                    StringBuilder output = new StringBuilder();
                    foreach (var item in rejected)
                    {
                        total -= item;
                        output.Append(item);
                        output.Append(", ");
                    }
                    output.Remove(output.Length - 2, 2);
                    Console.WriteLine(output);

                    if (total == 0) Console.WriteLine("All coins were invalid!");
                    else Console.WriteLine("Successfully taken the remaining {0} coins with total amount of {1}¢!", amounts.Length - rejected.Count, total);
                }
            }
            else Console.WriteLine("Invalid amounts!");
        }

        static void TakeBatchOfBills(string[] words, CashDesk.CashDesk desk)
        {
            Bill[] amounts = new Bill[words.Length - 2];
            bool validInput = true;
            int total = 0;
            for (int i = 0; i < words.Length - 2; i++)
            {
                int curAmount;
                if (int.TryParse(words[i + 2], out curAmount))
                {
                    amounts[i] = new Bill(curAmount);
                    total += curAmount;
                }
                else
                {
                    validInput = false;
                    break;
                }
            }
            if (validInput)
            {
                List<int> rejected;
                desk.TakeMoney(new BatchBill(amounts), out rejected);
                if (rejected.Count == 0) Console.WriteLine("Successfully taken {0} bills with total amount of {1}$!", amounts.Length, total);
                else
                {
                    Console.Write("Invalid bills were rejected: ");
                    StringBuilder output = new StringBuilder();
                    foreach (var item in rejected)
                    {
                        total -= item;
                        output.Append(item);
                        output.Append(", ");
                    }
                    output.Remove(output.Length - 2, 2);
                    Console.WriteLine(output);

                    if (total == 0) Console.WriteLine("All bills were invalid!");
                    else Console.WriteLine("Successfully taken the remaining {0} bills with total amount of {1}$!", amounts.Length - rejected.Count, total);
                }
            }
            else Console.WriteLine("Invalid amounts!");
        }

        static void ChangeMoney(string[] words, CashDesk.CashDesk desk)
        {
            Coin[] amounts = new Coin[words.Length - 1];
            bool validInput = true;
            int total = 0;
            for (int i = 0; i < words.Length - 1; i++)
            {
                int curAmount;
                if (int.TryParse(words[i + 1], out curAmount))
                {
                    amounts[i] = new Coin(curAmount);
                    total += curAmount;
                }
                else
                {
                    validInput = false;
                    break;
                }
            }
            if (validInput)
            {
                List<int> rejected;
                string output = desk.GiveChange(new BatchCoin(amounts), out rejected);
                if (rejected.Count > 0) Console.WriteLine("Invalid coins were rejected: {0}", string.Join(", ", rejected));
                Console.WriteLine(output);
            }
            else Console.WriteLine("Invalid amounts!");
        }

        static void SellItem(string[] words, CashDesk.CashDesk desk)
        {
            int billsAmount, coinsAmount;
            if (int.TryParse(words[1], out billsAmount) && int.TryParse(words[2], out coinsAmount))
            {
                if (coinsAmount < 100)
                {
                    Console.Write("Input batch of bills: ");
                    string[] billsInput = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Bill[] amountOfBills = new Bill[billsInput.Length];
                    bool validInput = true;
                    int totalBills = 0;
                    for (int i = 0; i < billsInput.Length; i++)
                    {
                        int curAmount;
                        if (int.TryParse(billsInput[i], out curAmount))
                        {
                            amountOfBills[i] = new Bill(curAmount);
                            totalBills += curAmount;
                        }
                        else
                        {
                            validInput = false;
                            break;
                        }
                    }
                    if (validInput)
                    {

                        Console.Write("Input batch of coins: ");
                        string[] coinsInput = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        Coin[] amountOfCoins = new Coin[coinsInput.Length];
                        validInput = true;
                        int totalCoins = 0;
                        for (int i = 0; i < coinsInput.Length; i++)
                        {
                            int curAmount;
                            if (int.TryParse(coinsInput[i], out curAmount))
                            {
                                amountOfCoins[i] = new Coin(curAmount);
                                totalCoins += curAmount;
                            }
                            else
                            {
                                validInput = false;
                                break;
                            }
                        }
                        if (validInput)
                        {
                            List<int> rejectedBills, rejectedCoins;
                            string output = desk.Sell(billsAmount, coinsAmount, new BatchBill(amountOfBills), new BatchCoin(amountOfCoins), out rejectedBills, out rejectedCoins);

                            if (rejectedBills.Count > 0) Console.WriteLine("Invalid bills were rejected: {0}", string.Join(", ", rejectedBills));
                            if (rejectedCoins.Count > 0) Console.WriteLine("Invalid coins were rejected: {0}", string.Join(", ", rejectedCoins));

                            Console.WriteLine(output);
                        }
                        else Console.WriteLine("Invalid amounts!");
                    }
                    else Console.WriteLine("Invalid amounts!");
                }
                else Console.WriteLine("Invalid amounts!");
            }
            else Console.WriteLine("Invalid amounts!");
        }

        static void Main(string[] args)
        {
            //Commands for testing the application!
            //-------------------------------------
            //takebill 10
            //takebatch bills 1 1 1 2 2 2 5 5 510 20 50 100 100
            //takebill 100
            //total
            //inspect
            //takecoin 10
            //takebatch coins 2 5 10 20 50 100 100
            //takecoin 100
            //total
            //inspect
            //givechange 10 20 50
            //givechange 10 20 50 50 2
            //givechange 50 50 50 20 20 20 20 10 10
            //inspect
            //sell 23 45
            //20 5
            //50
            //inspect
            //sell 23 45
            //20 5
            //50
            //inspect

            var desk = new CashDesk.CashDesk();

            Console.WriteLine("takecoin <number> - adds a bill with value <number> to the cashdesk");
            Console.WriteLine("takebill <number> - adds a bill with value <number> to the cashdesk");
            Console.WriteLine("takebatch coins <number1> <number2> ... - adds a batch of bills to the cashdesk");
            Console.WriteLine("takebatch bills <number1> <number2> ... - adds a batch of bills to the cashdesk");
            Console.WriteLine("givechange <number1> <number2> ... - takes a batch of coins and returs a batch of bills if possible");
            Console.WriteLine("-------------------------");
            Console.WriteLine("sell <number1> <number2>   - sells item with value of <number1> dollars and <number2> cents(< 100)");
            Console.WriteLine("<number1> <number2> ...    - given batch of bills for the item");
            Console.WriteLine("<number1> <number2> ...    - given batch of coins for the item");
            Console.WriteLine("-------------------------");
            Console.WriteLine("total - prints the total money in the cash desk");
            Console.WriteLine("inspect - prints detailed information of the money in the cashdesk");
            Console.WriteLine("exit - exits the application");
            Console.WriteLine();

            string command = string.Empty;
            while (command != "exit")
            {
                command = Console.ReadLine();
                string[] words = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length == 0) continue;

                switch (words[0].ToLower())
                {
                    case "takebill":
                        {
                            if (words.Length > 1)
                            {
                                int amount = int.TryParse(words[1], out amount) ? amount : 0;
                                if (amount > 0)
                                {
                                    if (desk.TakeMoney(new Bill(amount))) Console.WriteLine("Successfully taken {0}$ bill!", amount);
                                    else Console.WriteLine("Invalid bill!");
                                }
                                else Console.WriteLine("Invalid amount!");
                            }
                            else Console.WriteLine("No amount given!");
                            break;
                        }
                    case "takecoin":
                        {
                            if (words.Length > 1)
                            {
                                int amount = int.TryParse(words[1], out amount) ? amount : 0;
                                if (amount > 0)
                                {
                                    if (desk.TakeMoney(new Coin(amount))) Console.WriteLine("Successfully taken {0}¢ coin!", amount);
                                    else Console.WriteLine("Invalid coin!");
                                }
                                else Console.WriteLine("Invalid amount!");
                            }
                            else Console.WriteLine("No amount given!");
                            break;
                        }
                    case "takebatch":
                        {
                            if (words.Length == 1) Console.WriteLine("Invalid command!");
                            else
                            {
                                string batchType = words[1].ToLower();

                                if (batchType != "coins" && batchType != "bills") Console.WriteLine("Invalid command!");
                                else if (words.Length == 2) Console.WriteLine("No amounts given!");
                                else if (batchType == "coins")
                                {
                                    TakeBatchOfCoins(words, desk);
                                }
                                else if (batchType == "bills")
                                {
                                    TakeBatchOfBills(words, desk);
                                }
                            }
                            break;
                        }
                    case "givechange":
                        {
                            ChangeMoney(words, desk);
                            break;
                        }
                    case "sell":
                        {
                            if (words.Length > 2)
                            {
                                SellItem(words, desk);
                            }
                            else Console.WriteLine("Invalid command!");
                            break;
                        }
                    case "total":
                        {
                            int totalBills, totalCoins;
                            desk.Total(out totalBills, out totalCoins);
                            Console.WriteLine("Total {0}$ and {1}¢ in desk.", totalBills, totalCoins);
                            break;
                        }
                    case "inspect":
                        {
                            desk.Inspect();
                            break;
                        }
                    case "exit":
                        {
                            command = "exit";
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command!");
                            break;
                        }
                }
            }
        }
    }
}
