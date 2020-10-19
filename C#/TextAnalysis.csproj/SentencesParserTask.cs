using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            return SplitWords(SplitOffers(text));
        }

        public static List<string> SplitOffers(string text)
        {
            List<string> offers = new List<string>();
            var temp = "";
            var k = 0;
            foreach (char elements in text)
            {
                var stop = new char[] { '.', '!', '?', ';', ':', '(', ')' };
                temp += elements;
                if (stop.Contains(elements) && k == 0)
                {
                    offers.Insert(k, temp);
                    k++;
                    temp = "";
                }
                else if (stop.Contains(elements))
                {
                    offers.Insert(k, temp.Substring(1, temp.Length - 1));
                    k++;
                    temp = "";
                }
            }
            return offers;
        }

        public static List<List<string>> SplitWords(List<string> offers)
        {
            List<List<string>> sentencesList = new List<List<string>>();
            List<string> wordsList = new List<string>();
            var line = "";
            var numOffers = 0;

            for (int i = 0; i < offers.Count; i++)
            {
                offers[numOffers].ToLower();
                foreach (char elements in offers[numOffers])
                {
                    if (char.IsLetter(elements) || elements == '\'')
                    {
                        line += elements;
                        wordsList.Add(line);
                    }
                    line = "";
                }
                sentencesList.Insert(i, wordsList);
                numOffers++;
            }
            return sentencesList;
        }
    }
}