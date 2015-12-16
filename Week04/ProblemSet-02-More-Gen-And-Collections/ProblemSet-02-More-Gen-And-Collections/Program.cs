using System;
using GenericCollections;

namespace ProblemSet_02_More_Gen_And_Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---LinkedList test---");

            var list = new LinkedList<string>();
            list.Add("x");
            list.Add("g");
            list.Add("s");

            Console.WriteLine(list.Count); //output: 3

            list.InsertAfter("g", "a");
            list.InsertBefore("g", "a");
            Console.WriteLine(list.InsertAt(10, "z")); //output: False
            list.InsertAt(2, "z");
            list.Remove("z");
            list[1] = "m";

            foreach (string value in list)
            {
                Console.WriteLine(value);
            }
            //output:
            //x
            //m
            //g
            //a
            //s

            Console.WriteLine("---DynamicArray test---");

            var dArray = new DynamicArray<string>();
            dArray.Add("x");
            dArray.Add("g");
            dArray.Add("s");

            Console.WriteLine(dArray.Count); //output: 3

            dArray.InsertAt(1, "a");
            dArray.InsertAt(3, "a");
            Console.WriteLine(dArray.InsertAt(10, "z")); //output: False
            dArray.InsertAt(2, "z");
            dArray.Remove("z");
            dArray[1] = "m";

            for (int i = 0; i < dArray.Count; i++)
            {
                Console.WriteLine(dArray[i]);
            }
            //output:
            //x
            //m
            //g
            //a
            //s

            Console.WriteLine("---Map test---");

            var map = new Map<int, string>();

            map.Add(1, "a");
            map.Add(2, "a");
            map.Add(3, "s");

            Console.WriteLine(map.Count); //output: 3

            try
            {
                map.Add(2, "v");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message); //exception message
            }

            Console.WriteLine(map.ContainsKey(2)); //output: True
            Console.WriteLine(map.ContainsValue("a")); //output: True
            map.Remove(2);
            Console.WriteLine(map.ContainsKey(2)); //output: False
            Console.WriteLine(map.ContainsValue("a")); //output: True

            Console.WriteLine(map.Count); //output: 2

            Console.WriteLine("---Hash Map test---");

            var hashMap = new Map<int, string>();

            hashMap.Add(1, "a");
            hashMap.Add(2, "a");
            hashMap.Add(3, "s");

            Console.WriteLine(hashMap.Count); //output: 3

            try
            {
                hashMap.Add(2, "v");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message); //exception message
            }

            Console.WriteLine(hashMap.ContainsKey(2)); //output: True
            Console.WriteLine(hashMap.ContainsValue("a")); //output: True
            hashMap.Remove(2);
            Console.WriteLine(hashMap.ContainsKey(2)); //output: False
            Console.WriteLine(hashMap.ContainsValue("a")); //output: True

            Console.WriteLine(hashMap.Count); //output: 2
            
            Console.ReadKey();
        }
    }
}
