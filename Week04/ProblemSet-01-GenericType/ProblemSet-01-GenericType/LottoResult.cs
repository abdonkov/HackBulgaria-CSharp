using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_GenericType
{
    class LottoResult<T1, T2>
    {
        private readonly bool isWinning;
        private readonly byte matchedNumbersCount;


        public bool IsWinning { get { return isWinning; } }
        public byte MatchedNumbersCount { get { return matchedNumbersCount; } }

        public LottoResult()
        {
            isWinning = false;
            matchedNumbersCount = 0;
        }

        public LottoResult(Combination<T1, T2> combinationToCheck, Combination<T1, T2> winningCombination)
        {
            matchedNumbersCount = 0;
            if (object.Equals(combinationToCheck.FirstItem, winningCombination.FirstItem)) matchedNumbersCount++;
            if (object.Equals(combinationToCheck.SecondItem, winningCombination.SecondItem)) matchedNumbersCount++;
            if (object.Equals(combinationToCheck.ThirdItem, winningCombination.ThirdItem)) matchedNumbersCount++;
            if (object.Equals(combinationToCheck.ForthItem, winningCombination.ForthItem)) matchedNumbersCount++;
            if (object.Equals(combinationToCheck.FifthItem, winningCombination.FifthItem)) matchedNumbersCount++;
            if (object.Equals(combinationToCheck.SixthItem, winningCombination.SixthItem)) matchedNumbersCount++;
            if (matchedNumbersCount > 0) isWinning = true;
        }
    }
}
