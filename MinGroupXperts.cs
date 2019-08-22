using System;
using System.Collections.Generic;
using System.Linq;

namespace IT
{
    public class MinGroupXperts
    {
        private List<Xpert> I0;
        private List<uint> T0;
        private List<(List<uint> questions,uint idX)> T0i;

        private List<(Xpert xpert, float weight)> Weights()
        {
            List<(Xpert xpert, float weight)> pi = new List<(Xpert xpert, float weight)>();

            float total = 0;

            foreach(var item in T0i)
            {
                total += item.questions.Count;
            }

            foreach(var x in I0)
            {
                pi.Add((x, (T0i.Find(i => i.idX == x.Id).questions.Count / total)));
            }
            pi.Sort((l, r) => l.weight.CompareTo(r.weight));
            return pi;
        }
        private void EraseQuestion(int idXpert)
        {

        }
        public MinGroupXperts(List<Xpert> _I0, List<uint> _T0, List<(List<uint> questions, uint idX)> _T0i)
        {
            I0 = _I0;
            T0 = _T0;
            T0i = _T0i;
        }

        public List<Xpert> Evaluate()
        {
            bool end = false;

            while(!end)
            {
                var weights = Weights();

                if (weights.Count == 1)
                    break;

                for(int i = 0; i < weights.Count - 1; ++i)
                {
                    if(weights[i].weight != weights[i+1].weight)
                    {
                        break;
                    }
                    end |= i == weights.Count - 2;                    
                }

                if (end)
                    break;

                Xpert xpert = weights[0].xpert;

                I0.Remove(xpert);

                var questions = T0i.Find(x => x.idX == xpert.Id).questions;

                //foreach (var item in T0i)
                //{
                //    item.questions.Except(questions);
                //}

                for(int i = 0; i < T0i.Count; ++i)
                {
                    T0i[i].questions = T0i[i].questions.Except(questions);
                }

                T0.Except(questions);
                
            }
            
            return I0;
        }
    }
}
