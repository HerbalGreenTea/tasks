//я педик
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private Dictionary<int, string[]> listDocuments = new Dictionary<int, string[]>();
        public void Add(int id, string documentText)
        {
            var docText = documentText.Split(' ', '.', ',', '!', '?', ':', '-', '\r', '\n');
            listDocuments.Add(id, docText);
        }

        public List<int> GetIds(string word)
        {
            var listID = new List<int>();
            foreach (int key in listDocuments.Keys)
                if (listDocuments[key].Contains(word))
                    listID.Add(key);

            return listID;
        }

        public List<int> GetPositions(int id, string word)
        {
            
        }

        public void Remove(int id)
        {
            listDocuments.Remove(id);
        }
    }
}
