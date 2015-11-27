using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace BankAccountBalance
{
    class Program
    {
        struct BankTransaction
        {
            public DateTime Date;
            public string Operation;
            public double Amount;

            public BankTransaction(DateTime date, string operation, double amount)
            {
                Date = date;
                Operation = operation;
                Amount = amount;
            }
        }

        static List<BankTransaction> ReadTransactions(string filename)
        {
            string[] lines = { "" };
            try
            {
                lines = File.ReadAllLines(filename);
            }
            catch
            {
                Console.WriteLine("{0} not found!", filename);
                return null;
            }

            List<BankTransaction> transactions = new List<BankTransaction>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] curLine = { "" };
                curLine = lines[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                DateTime dt = new DateTime();
                double amount = 0;
                try
                {
                    dt = DateTime.Parse(curLine[0].Replace(".", "/").Substring(0, curLine[0].Length - 1));
                    amount = double.Parse(curLine[2].Replace(".", ",").Substring(0, curLine[2].Length - 2));
                }
                catch
                {
                    Console.WriteLine("{0} has invalid format!", filename);
                    return null;
                }

                if (curLine[1] == "теглене" || curLine[1] == "внасяне")
                {
                    transactions.Add(new BankTransaction(dt, curLine[1], amount));
                }
                else
                {
                    Console.WriteLine("{0} has invalid format!", filename);
                    return null;
                }
            }

            return transactions;
        }

        static void MoneyUsageInDateRange(List<BankTransaction> transactions, DateTime startDate, DateTime endDate)
        {
            double received = 0;
            double spent = 0;

            foreach (BankTransaction trans in transactions)
            {
                if (trans.Date >= startDate && trans.Date <= endDate)
                {
                    if (trans.Operation == "теглене") spent += trans.Amount;
                    else if (trans.Operation == "внасяне") received += trans.Amount;
                }
            }

            Console.WriteLine("Информация за период: {0:dd/MM/yyyy} - {1:dd/MM/yyyy}", startDate, endDate);
            Console.WriteLine("Изтеглени: {0:C2}", spent);
            Console.WriteLine("Внесени: {0:C2}", received);
            Console.WriteLine("Баланс: {0:C2}", received - spent);
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("bg-BG");
            List<BankTransaction> transactions = ReadTransactions("Pesho.txt");
            MoneyUsageInDateRange(transactions, new DateTime(2015, 4, 25), new DateTime(2015, 4, 30));

            Console.ReadKey();
        }
    }
}
