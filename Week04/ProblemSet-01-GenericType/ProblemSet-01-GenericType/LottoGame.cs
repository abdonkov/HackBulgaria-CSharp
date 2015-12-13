using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_GenericType
{
    class LottoGame<T1, T2>
    {
        private List<Combination<T1, T2>> combinations;
        private readonly Combination<T1, T2> winningCombination;

        public LottoGame(Combination<T1, T2> winningCombination)
        {
            this.winningCombination = winningCombination;
            combinations = new List<Combination<T1, T2>>();
        }

        public bool AddUserCombination(Combination<T1,T2> combinationToAdd)
        {
            foreach (var combination in combinations)
            {
                if (object.Equals(combination, combinationToAdd)) return false;
            }

            combinations.Add(combinationToAdd);
            return true;

        }

        private Combination<T1, T2> GetWinningCombination()
        {
            return winningCombination;
        }

        public LottoResult<T1, T2> Validate()
        {
            LottoResult<T1, T2> bestLottoResult = new LottoResult<T1, T2>();
            foreach (var combination in combinations)
            {
                var curLottoResult = new LottoResult<T1, T2>(combination, winningCombination);
                if (bestLottoResult.MatchedNumbersCount < curLottoResult.MatchedNumbersCount) bestLottoResult = curLottoResult;
            }
            return bestLottoResult;
        }
    }
}
