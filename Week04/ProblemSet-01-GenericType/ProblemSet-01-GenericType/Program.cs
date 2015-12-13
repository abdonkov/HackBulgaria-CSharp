using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_GenericType
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---Generic Stack Test---");

            GenericStack<int> stack = new GenericStack<int>(0);

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            while(!stack.IsEmpty())
            {
                Console.WriteLine(stack.Contains(2));
                Console.WriteLine(stack.Peek());
                Console.WriteLine(stack.Pop());
            }

            Console.WriteLine("---Generic Dequeue Test---");

            GenericDequeue<int> dequeue = new GenericDequeue<int>(0);

            dequeue.AddToEnd(3);
            dequeue.AddToEnd(4);
            dequeue.AddToFront(2);
            dequeue.AddToFront(1);

            while (!dequeue.IsEmpty())
            {
                Console.WriteLine(dequeue.Contains(3));
                Console.WriteLine(dequeue.PeekFromEnd());
                Console.WriteLine(dequeue.RemoveFromEnd());
                Console.WriteLine(dequeue.Contains(3));
                Console.WriteLine(dequeue.PeekFromFront());
                Console.WriteLine(dequeue.RemoveFromFront());
            }

            Console.WriteLine("---Lotto Game Test---");

            LottoGame<int, string> lottoGame = new LottoGame<int, string>(new Combination<int, string>(1, 2, 3, "a", "b", "c"));

            var comb1 = new Combination<int, string>(5, 7, 9, "we", "asd", "rgd");
            var comb2 = new Combination<int, string>(5, 7, 8, "we", "asd", "rgd");
            var comb3 = new Combination<int, string>(5, 7, 9, "we", "asd", "rgd");
            var comb4 = new Combination<int, string>(1, 7, 9, "we", "asd", "rgd");
            var comb5 = new Combination<int, string>(5, 2, 9, "we", "b", "rgd");

            if (lottoGame.AddUserCombination(comb1)) Console.WriteLine("Added combination {0}", comb1);
            else Console.WriteLine("Combination {0} already exists!", comb1);
            if (lottoGame.AddUserCombination(comb2)) Console.WriteLine("Added combination {0}", comb2);
            else Console.WriteLine("Combination {0} already exists!", comb2);
            if (lottoGame.AddUserCombination(comb3)) Console.WriteLine("Added combination {0}", comb3);
            else Console.WriteLine("Combination {0} already exists!", comb3);
            if (lottoGame.AddUserCombination(comb4)) Console.WriteLine("Added combination {0}", comb4);
            else Console.WriteLine("Combination {0} already exists!", comb4);
            if (lottoGame.AddUserCombination(comb5)) Console.WriteLine("Added combination {0}", comb5);
            else Console.WriteLine("Combination {0} already exists!", comb5);

            Console.WriteLine();

            var lottoResult = lottoGame.Validate();
            if (lottoResult.IsWinning) Console.WriteLine("This lotto game has a winner with {0} matching values!", lottoResult.MatchedNumbersCount);
            else Console.WriteLine("There is no winner in this lotto game!");

            Console.ReadKey();
        }
    }
}
