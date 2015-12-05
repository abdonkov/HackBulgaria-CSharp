
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDesk
{
    public class BatchCoin : IEnumerable
    {
        private readonly List<Coin> batch;

        public int Count { get { return batch.Count; } }

        public BatchCoin(List<Coin> coins)
        {
            batch = new List<Coin>(coins.Count);
            foreach (var coin in coins)
            {
                batch.Add(coin);
            }
        }

        public BatchCoin(Coin[] coins) : this(coins.ToList()) { }

        public int Total()
        {
            return batch.Sum(item => item.Amount);
        }

        public override string ToString()
        {
            return string.Format("{0} coins with total value {1}¢", Count, Total());
        }

        public IEnumerator GetEnumerator()
        {
            return batch.GetEnumerator();
        }
    }
}
