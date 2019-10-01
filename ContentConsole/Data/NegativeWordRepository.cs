using System.Collections.Generic;
using System.Linq;

namespace ContentConsole.Data
{
    public class NegativeWordRepository : INegativeWordRepository
    {
        private readonly List<string> _listOfNegativeWords;

        public NegativeWordRepository()
        {
            _listOfNegativeWords = new List<string>
            {
                "swine",
                "bad",
                "nasty",
                "horrible"
            };
        }

        public void AddAWord(string wordToAdd)
        {
            _listOfNegativeWords.Add(wordToAdd);
        }

        public List<string> GetListOfNegativeWords()
        {
            return _listOfNegativeWords.ToList();
        }

        public void RemoveAWord(string removeAWord)
        {
            _listOfNegativeWords.Remove(removeAWord);
        }

    }
}
