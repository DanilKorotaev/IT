using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DBHelper DB = new DBHelper();
            //List<Xpert> I0 = new List<Xpert> { new Xpert(1),
            //                                   new Xpert(2),
            //                                   new Xpert(3),
            //                                   new Xpert(4),
            //                                   new Xpert(5)
            //                                  };

            //List<uint> T0 = new List<uint>
            //{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //List<KeyValuePair<uint, List<uint>>> T0i = new List<KeyValuePair<uint, List<uint>>> 
            //{
            //     new KeyValuePair<uint, List<uint>>( 1, new List<uint> { 1, 2, 3, 4, 5 }),
            //     new KeyValuePair<uint, List<uint>>( 2, new List<uint> { 2, 3, 4, 5, 6, 7 }),
            //     new KeyValuePair<uint, List<uint>>( 3, new List<uint> { 5, 6, 7, 8 }),
            //     new KeyValuePair<uint, List<uint>>( 4,new List<uint> { 4, 5, 6, 7, 8, 9, 10 }),
            //     new KeyValuePair<uint, List<uint>>( 5,new List<uint> { 1, 2, 3, 4, 5, 7, 9, 10 })
            //};
            var I0 = DB.GetXperts();

            var question = DB.GetQuestion();

            var T0i = DB.GetCompetence();

            List<uint> T0 = (List<uint>)question.Select(x => x.Id);

            MinGroupXperts keklol = new MinGroupXperts(I0,T0,T0i);
            var items = keklol.Evaluate();

            Console.WriteLine(keklol.Сonclusion);

            foreach(var item in items)
            {
                Console.WriteLine(item.Id);
            }

            keklol.ToDocFile("test.docx");

            Console.WriteLine(keklol.FullSolution);
        }
    }
}
