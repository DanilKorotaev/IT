using System;
using System.Collections.Generic;
using System.Linq;

namespace IT
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            List<int> listint1 = new List<int>{ 1, 2, 3, 4, 5 };

            List<int> listint2 = new List<int> { 4, 5 };


            listint1.Except(listint2);

            List<Xpert> I0 = new List<Xpert> { new Xpert(1),
                                               new Xpert(2),
                                               new Xpert(3),
                                               new Xpert(4),
                                               new Xpert(5)
                                              };

            List<uint> T0 = new List<uint>
            { 1U, 2U, 3U, 4U, 5U, 6U, 7U, 8U, 9U, 10U };


            List<(List<uint> questions, uint idX)> T0i = new List<(List<uint> questions, uint idX)>
            {
                 (new List<uint> { 1, 2, 3, 4, 5 }, 1),
                 (new List<uint> { 2, 3, 4, 5, 6, 7 }, 2),
                 (new List<uint> { 5, 6, 7, 8 }, 3),
                 (new List<uint> { 4, 5, 6, 7, 8, 9, 10 }, 4),
                 (new List<uint> { 1, 2, 3, 4, 5, 7, 9, 10 }, 5)
            };

            MinGroupXperts keklol = new MinGroupXperts(I0,T0,T0i);
            var items = keklol.Evaluate();

            foreach(var item in items)
            {
                Console.WriteLine(item.Id);
            }
        //Console.WriteLine("Hello World!");
        }
    }
}
