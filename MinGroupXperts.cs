using System;
using System.Collections.Generic;
using System.Linq;

namespace IT
{
    public class MinGroupXperts
    {
        private List<Xpert> I0;
        private List<uint> T0;
        private List<KeyValuePair<uint ,List<uint>>> T0i;
        int paragraph = 1;

        private List<(Xpert xpert, float weight)> Weights()
        {
            FullSolution += paragraph++ + " Определим веса экспертов:" + Environment.NewLine;

            List<(Xpert xpert, float weight)> pi = new List<(Xpert xpert, float weight)>();

            float total = 0;

            foreach(var item in T0i)
            {
                total += item.Value.Count;
            }
            
            foreach(var x in I0)
            {
                FullSolution += $"- определим вес эксперта {x.Id}" + Environment.NewLine;
                int countQuestion = T0i.Find(i => i.Key == x.Id).Value.Count;
                float p = countQuestion / total;
                FullSolution += $"p{x.Id} = {countQuestion} / {total} = {p}" + Environment.NewLine;
                pi.Add((x,  p));
            }

            pi.Sort((l, r) => l.weight.CompareTo(r.weight));

            FullSolution += " Упорядочим веса" + Environment.NewLine;
            
            foreach(var p in pi)
            {
                FullSolution += $"p{p.xpert.Id} = {p.weight}; ";
            }
            FullSolution += Environment.NewLine;
            return pi;
        }

        public MinGroupXperts(List<Xpert> _I0, List<uint> _T0, List<KeyValuePair<uint, List<uint>>> _T0i)
        {
            I0 = _I0;
            T0 = _T0;
            T0i = _T0i;
        }

        public string FullSolution { get; private set; } = "";

        private string InfoState()
        {
            string result = "I0 = {";

            I0.ForEach(x => result += x.Id + ",");

            result += "\b} - множество всех экспертов" + Environment.NewLine;

            result += "T0i - множество всех вопросов, на которые может ответить i-ый эксперт" + Environment.NewLine;

            T0i.ForEach(x => {
                result += $"T0{x.Key} = {{";
                x.Value.ForEach(y => result += $"{y},");
                result += "\b};" + Environment.NewLine;
            });

            return result;
        }

        public List<Xpert> Evaluate()
        {
            bool end = false;       

            FullSolution += "Задано" + Environment.NewLine;

            FullSolution += InfoState();

            FullSolution += "Определить минимально возможную группу экспертов методом случайного поиска" + Environment.NewLine;

            List<(Xpert xpert, float weight)> weights = null;

            while (!end)
            {       
                weights = Weights();

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

                FullSolution += paragraph++ + $". Из множества экспертов исключаем эксперта {xpert.Id}." + Environment.NewLine;
                FullSolution += "Из множества вопросов исключаются вопросы {";
                var questions = T0i.Find(x => x.Key == xpert.Id).Value;

                questions.ForEach(x => FullSolution += $"{x},");
                FullSolution += "\b}" + Environment.NewLine;

                for (int i = T0i.Count -1; i >= 0; --i)
                {
                    T0i[i] = new KeyValuePair<uint, List<uint>>(T0i[i].Key, new List<uint>(T0i[i].Value.Except(questions)));
                    if(T0i[i].Value.Count == 0)
                    {
                        T0i.RemoveAt(i);
                    }
                }

                T0 = new List<uint>(T0.Except(questions));
                FullSolution += paragraph++ + " Тогда остается: " + Environment.NewLine;
                FullSolution += InfoState();
            }

            FullSolution += paragraph++ + "Таким образом можно сделать вывод о том," +
                " что для дачи заключения по указанным вопросам из всех экспертов достаточно всего " + weights.Count +": ";

            weights.ForEach(x => FullSolution += " " + x.xpert.Id + ",");

            FullSolution += "\b.";

            return I0;
        }
    }
}
