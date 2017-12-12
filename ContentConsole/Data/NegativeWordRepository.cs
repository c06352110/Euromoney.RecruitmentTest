using System.Collections.Generic;
using System.Linq;
using ContentConsole.Data;

namespace ContentConsole
{
    public class NegativeWordRepository : INegativeWordRepository
    {
        private readonly List<string> _listOfNegativeWords;

        public NegativeWordRepository(List<string> listOfNegativeWords)
        {
            _listOfNegativeWords = listOfNegativeWords;
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
