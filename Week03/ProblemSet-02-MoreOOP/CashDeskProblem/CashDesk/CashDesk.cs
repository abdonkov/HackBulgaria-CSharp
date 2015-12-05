using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDesk
{
    public class CashDesk
    {
        private SortedDictionary<int, int> bills;
        private SortedDictionary<int, int> coins;

        public CashDesk()
        {
            bills = new SortedDictionary<int, int>();
            bills.Add(1, 0);
            bills.Add(2, 0);
            bills.Add(5, 0);
            bills.Add(10, 0);
            bills.Add(20, 0);
            bills.Add(50, 0);
            bills.Add(100, 0);
            coins = new SortedDictionary<int, int>();
            coins.Add(1, 0);
            coins.Add(2, 0);
            coins.Add(5, 0);
            coins.Add(10, 0);
            coins.Add(20, 0);
            coins.Add(50, 0);            
        }

        public bool TakeMoney(Coin coin)
        {
            if (!coins.ContainsKey(coin.Amount)) return false;

            coins[coin.Amount]++;
            return true;
        }

        public bool TakeMoney(BatchCoin batch, out List<int> rejected)
        {
            rejected = new List<int>();
            bool allCoinsAreCorrect = true;
            foreach (Coin coin in batch)
            {
                if (!TakeMoney(coin))
                {
                    allCoinsAreCorrect = false;
                    rejected.Add(coin.Amount);
                }
            }
            return allCoinsAreCorrect;
        }

        public bool TakeMoney(Bill bill)
        {
            if (!bills.ContainsKey(bill.Amount)) return false;

            bills[bill.Amount]++;
            return true;
        }

        public bool TakeMoney(BatchBill batch, out List<int> rejected)
        {
            rejected = new List<int>();
            bool allBillsAreCorrect = true;
            foreach (Bill bill in batch)
            {
                if (!TakeMoney(bill))
                {
                    allBillsAreCorrect = false;
                    rejected.Add(bill.Amount);
                }
            }
            return allBillsAreCorrect;
        }

        private bool GiveMoney(Coin coin)
        {
            if (!coins.ContainsKey(coin.Amount)) return false;

            coins[coin.Amount]--;
            return true;
        }

        private void GiveMoney(BatchCoin batch)
        {
            foreach (Coin coin in batch)
            {
                GiveMoney(coin);
            }
        }

        private bool GiveMoney(Bill bill)
        {
            if (!bills.ContainsKey(bill.Amount)) return false;

            bills[bill.Amount]--;
            return true;
        }

        private void GiveMoney(BatchBill batch)
        {
            foreach (Bill bill in batch)
            {
                GiveMoney(bill);
            }
        }

        private bool GreadyChange(int change, ref SortedList<int, int> availableAmounts, ref LinkedList<int> changeAmounts)
        {

            if (change == 0) return true;

            bool possible = false;
            for (int i = 0; i < availableAmounts.Count; i++)
            {
                KeyValuePair<int, int> curAmout = availableAmounts.ElementAt(i);
                if (curAmout.Value > 0)
                {
                    if (change - curAmout.Key >= 0)
                    {
                        availableAmounts[curAmout.Key]--;
                        changeAmounts.AddLast(curAmout.Key);
                        if (GreadyChange(change - curAmout.Key, ref availableAmounts, ref changeAmounts))
                        {
                            possible = true;
                            break;
                        }
                        else
                        {
                            changeAmounts.RemoveLast();
                            availableAmounts[curAmout.Key]++;
                        }
                    }
                }
            }

            if (possible) return true;
            else return false;
        }

        public string GiveChange(BatchCoin batch, out List<int> rejected)
        {
            int total = batch.Total();

            TakeMoney(batch, out rejected);

            SortedList<int, int> availableAmounts = new SortedList<int, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            foreach (KeyValuePair<int, int> coin in coins)
            {
                if (coin.Key <= total && coin.Value > 0)
                {
                    availableAmounts.Add(coin.Key, coin.Value);
                }
            }

            foreach (KeyValuePair<int, int> bill in bills)
            {
                if (bill.Key * 100 <= total && bill.Value > 0)
                {
                    availableAmounts.Add(bill.Key * 100, bill.Value);
                }
            }

            LinkedList<int> changeAmounts = new LinkedList<int>();

            int lowestBill = bills.Keys.ElementAt(0);

            if (GreadyChange(total, ref availableAmounts, ref changeAmounts))
            {
                if (changeAmounts.Count >= batch.Count)
                {
                    GiveMoney(batch);
                    return "No better batch to return!";
                }
                else
                {
                    StringBuilder returnedBatch = new StringBuilder();
                    foreach (int amount in changeAmounts)
                    {
                        if (amount >= lowestBill * 100)
                        {
                            returnedBatch.Append(amount / 100);
                            returnedBatch.Append("$ bills, ");
                            bills[amount / 100]--;
                        }
                        else
                        {
                            returnedBatch.Append(amount);
                            returnedBatch.Append("¢ coins, ");
                            coins[amount]--;
                        }
                    }
                    returnedBatch.Remove(returnedBatch.Length - 2, 2);

                    return returnedBatch.ToString();
                }
            }
            else
            {
                GiveMoney(batch);
                return "Change is not possible!";
            }
        }

        public string GiveChange(int changeToGive)
        {
            SortedList<int, int> availableAmounts = new SortedList<int, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            foreach (KeyValuePair<int, int> coin in coins)
            {
                if (coin.Key <= changeToGive && coin.Value > 0)
                {
                    availableAmounts.Add(coin.Key, coin.Value);
                }
            }

            foreach (KeyValuePair<int, int> bill in bills)
            {
                if (bill.Key * 100 <= changeToGive && bill.Value > 0)
                {
                    availableAmounts.Add(bill.Key * 100, bill.Value);
                }
            }

            LinkedList<int> changeAmounts = new LinkedList<int>();

            int lowestBill = bills.Keys.ElementAt(0);

            if (GreadyChange(changeToGive, ref availableAmounts, ref changeAmounts))
            {
                StringBuilder returnedBatch = new StringBuilder();
                foreach (int amount in changeAmounts)
                {
                    if (amount >= lowestBill * 100)
                    {
                        returnedBatch.Append(amount / 100);
                        returnedBatch.Append("$ bills, ");
                        bills[amount / 100]--;
                    }
                    else
                    {
                        returnedBatch.Append(amount);
                        returnedBatch.Append("¢ coins, ");
                        coins[amount]--;
                    }
                }
                returnedBatch.Remove(returnedBatch.Length - 2, 2);

                return returnedBatch.ToString();
            }
            else return "Cannot give back change!";
        }

        public string Sell(int dollarValue, int centValue, BatchBill batchOfBills, BatchCoin batchOfCoins, out List<int> rejectedBills, out List<int> rejectedCoins)
        {
            rejectedBills = new List<int>();
            rejectedCoins = new List<int>();

            List<Bill> validBills = new List<Bill>();
            int validBillsAmount = 0;

            foreach (Bill bill in batchOfBills)
            {
                if (bills.ContainsKey(bill.Amount))
                {
                    validBills.Add(bill);
                    validBillsAmount += bill.Amount;
                }
            }

            List<Coin> validCoins = new List<Coin>();
            int validCoinsAmount = 0;

            foreach (Coin coin in batchOfCoins)
            {
                if (coins.ContainsKey(coin.Amount))
                {
                    validCoins.Add(coin);
                    validCoinsAmount += coin.Amount;
                }
            }

            int validMoney = validBillsAmount * 100 + validCoinsAmount;
            int priceMoney = dollarValue * 100 + centValue;
            bool needToGiveChange = true;
            string output = String.Empty;

            if (validMoney < priceMoney) return "Not enough money!";
            else if (validMoney == priceMoney)
            {
                needToGiveChange = false;
                output = "Item sold successfully! No change needed!";
            }

            bool hasInvalidBills = !TakeMoney(batchOfCoins, out rejectedCoins);
            bool hasInvalidCoins = !TakeMoney(batchOfBills, out rejectedBills);

            if (needToGiveChange)
            {
                output = GiveChange(validMoney - priceMoney);
                bool changeIsGiven = output[0] == 'C' ? false : true;

                if (changeIsGiven) output = "Item sold successfully!\r\nGiven back change: " + output;
                else
                {
                    GiveMoney(batchOfBills);
                    GiveMoney(batchOfCoins);
                }
            }

            return output;
        }

        public void Total(out int totalBills, out int totalCoins)
        {
            totalBills = 0;
            foreach (KeyValuePair<int, int> item in bills)
            {
                totalBills += item.Key * item.Value;
            }

            totalCoins = 0;
            foreach (KeyValuePair<int, int> item in coins)
            {
                totalCoins += item.Key * item.Value;
            }
        }

        public void Inspect()
        {
            int totalBills, totalCoins;
            Total(out totalBills, out totalCoins);
            Console.WriteLine("We have a total of {0}$ and {1}¢ in the desk.", totalBills, totalCoins);

            if (totalCoins > 0)
            {
                Console.WriteLine("We have the following count of coins, sorted in ascending order:");
                foreach (KeyValuePair<int, int> item in coins)
                {
                    if (item.Value > 0) Console.WriteLine("{0}¢ coins - {1}", item.Key, item.Value);
                }
            }

            if (totalBills > 0)
            {
                Console.WriteLine("We have the following count of bills, sorted in ascending order:");
                foreach (KeyValuePair<int, int> item in bills)
                {
                    if (item.Value > 0) Console.WriteLine("{0}$ bills - {1}", item.Key, item.Value);
                }
            }
        }
    }
}
