using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsLibrary;

namespace Events
{
    class Program
    {
        static void AverageChangeHandle(object sender, decimal oldAverage, decimal newAverage)
        {
            Console.Write("--->Handler: ");
            Console.WriteLine("Average changed from {0} to {1}.", oldAverage, newAverage);
        }

        static void CollectionChangeHandle(object sender, ItemChangeType changeType, int changedItemIndex, string changedItemInfo = null)
        {
            Console.Write("--->Handler: ");
            switch (changeType)
            {
                case ItemChangeType.Add:
                    Console.WriteLine("Added item to collection on {0} index!", changedItemIndex);
                    break;
                case ItemChangeType.Insert:
                    Console.WriteLine("Inserted item in collection on {0} index!", changedItemIndex);
                    break;
                case ItemChangeType.Remove:
                    if(changedItemIndex == -1) Console.WriteLine("Collection items cleared!");
                    else Console.WriteLine("Removed item from collection being on {0} index!", changedItemIndex);
                    break;
                case ItemChangeType.Replace:
                    Console.WriteLine("Replaced item in collection being on {0} index!", changedItemIndex);
                    break;
                case ItemChangeType.ChangedProperty:
                    Console.WriteLine("Item on {0} index property {1} changed!", changedItemIndex, changedItemInfo);
                    break;
                default:
                    break;
            }
        }

        static void MessageReceivedHandle(object sender, EventArgs e)
        {
            ReceiveBufferEventArgs rbe = (ReceiveBufferEventArgs)e;
            Console.WriteLine("Received: {0}", rbe.Message);
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            AverageAggregator averageAgg = new AverageAggregator(AverageChangeHandle);

            Console.WriteLine("---AverageAggregator---");
            Console.WriteLine();

            Console.WriteLine("Adding number to aggregator: 2");
            averageAgg.AddNumber(2);
            Console.WriteLine("Adding number to aggregator: 2");
            averageAgg.AddNumber(2);
            Console.WriteLine("Adding number to aggregator: 8");
            averageAgg.AddNumber(8);
            Console.WriteLine("Adding number to aggregator: 4");
            averageAgg.AddNumber(4);
            Console.WriteLine("Adding number to aggregator: 4");
            averageAgg.AddNumber(4);
            Console.WriteLine("Adding number to aggregator: 4");
            averageAgg.AddNumber(4);
            Console.WriteLine("Adding number to aggregator: -24");
            averageAgg.AddNumber(-24);
            Console.WriteLine("Adding number to aggregator: 0");
            averageAgg.AddNumber(0);
            Console.WriteLine("Adding number to aggregator: 0");
            averageAgg.AddNumber(0);
            Console.WriteLine("Adding number to aggregator: 10");
            averageAgg.AddNumber(10);
            Console.WriteLine("Adding number to aggregator: 1");
            averageAgg.AddNumber(1);
            Console.WriteLine("Adding number to aggregator: 1");
            averageAgg.AddNumber(1);
            Console.WriteLine("Adding number to aggregator: 2");
            averageAgg.AddNumber(2);

            Console.WriteLine();
            Console.WriteLine("---NotifyCollection---");
            Console.WriteLine();

            NotifyCollection<Book> notCol = new NotifyCollection<Book>(CollectionChangeHandle);

            Book book1 = new Book("author1", "book1", 2000);
            Book book2 = new Book("author2", "book2", 2001);
            Book book3 = new Book("author3", "book3", 2002);
            Console.WriteLine("Adding {0}!", book1);
            notCol.Add(book1);
            Console.WriteLine("Adding {0}!", book2);
            notCol.Add(book2);
            Console.WriteLine("Inserting {0} on index 1!", book3);
            notCol.Insert(1, book3);
            Console.WriteLine("Changing {0} year to 2011!", book1);
            book1.Year = 2011;
            Console.WriteLine("Changing {0} year to 2005!", book3);
            book3.Year = 2005;
            Console.WriteLine("Removing {0}!", book2);
            notCol.Remove(book2);
            Console.WriteLine("Changing removed book year to 1995!");
            book2.Year = 1995;
            Console.WriteLine("Clearing all items!");
            notCol.Clear();
            Console.WriteLine("Adding {0}!", book1);
            notCol.Add(book1);
            Console.WriteLine("Adding {0}!", book2);
            notCol.Add(book2);
            Console.WriteLine("Replacing book on 0 index with {0}", book3);
            notCol[0] = book3;
            Console.WriteLine("Changing old book year to 1895!");
            book1.Year = 1895;
            Console.WriteLine("Changing new book year to 2003!");
            book3.Year = 2003;

            Console.WriteLine();
            Console.WriteLine("---Network Buffer---");

            ReceiveBuffer rec = new ReceiveBuffer(MessageReceivedHandle);
            PacketGenerator pg = new PacketGenerator(rec);
            Console.WriteLine("Write messages. Write exit for exit.");
            Console.Write("Message to send: ");
            string inputMessage = Console.ReadLine();
            while(!inputMessage.Equals("exit"))
            {
                pg.SendMessage(inputMessage);
                Console.Write("Message to send: ");
                inputMessage = Console.ReadLine();
            }
        }
    }
}
