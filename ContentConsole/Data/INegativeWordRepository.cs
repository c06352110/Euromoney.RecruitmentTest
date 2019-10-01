using System.Collections.Generic;
using ContentConsole._ioc;

namespace ContentConsole.Data
{
    public interface INegativeWordRepository : IInjectable
    {
        void AddAWord(string wordToAdd);
        List<string> GetListOfNegativeWords();
        void RemoveAWord(string removeAWord);
    }
}