using System.Collections.Generic;

namespace ContentConsole.Data
{
    public interface INegativeWordRepository
    {
        void AddAWord(string wordToAdd);
        List<string> GetListOfNegativeWords();
        void RemoveAWord(string removeAWord);
    }
}