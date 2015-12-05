using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDesk
{
    public class BatchBill : IEnumerable
    {
        private readonly List<Bill> batch;

        public int Count { get { return batch.Count; } }

        public BatchBill(List<Bill> bills)
        {
            batch = new List<Bill>(bills.Count);
            foreach (var bill in bills)
            {
                batch.Add(bill);
            }
        }

        public BatchBill(Bill[] bills) : this(bills.ToList()) { }

        public int Total()
        {
            return batch.Sum(item => item.Amount);
        }

        public override string ToString()
        {
            return string.Format("{0} bills with total value {1}$", Count, Total());
        }

        public IEnumerator GetEnumerator()
        {
            return batch.GetEnumerator();
        }


    }
}
