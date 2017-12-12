using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ContentConsole.Data;

namespace ContentConsole.Filter
{
    public class FilterBySplittingWordsStrategy : IFilterStrategy
    {
        private readonly IEnumerable<string> _listOfNegativeWords;
        private readonly bool _disabledFiltering;

        public FilterBySplittingWordsStrategy(INegativeWordRepository negativeWordRepository, bool disabledFiltering = false)
        {
            _listOfNegativeWords = negativeWordRepository.GetListOfNegativeWords().Select(x => x.ToLower());
            _disabledFiltering = disabledFiltering;
        }

        public int CountNegativeWords(string content)
        {
            var contentSplitIntoLowerCaseWords = SplitContentIntoLowerCaseWords(content);

            return contentSplitIntoLowerCaseWords.Count(x => _listOfNegativeWords.Any(
                y => y.Equals(RemoveSymbolsAndNumbers(x))));
        }

        public string FilterNegativeWords(string content)
        {
            if (_disabledFiltering)
            {
                return content;
            }

            var filteredContent = new StringBuilder();
            var contentSplitIntoWords = SplitContentIntoLowerCaseWords(content).ToList();

            var count = 0;
            foreach (var contentSplitIntoWord in contentSplitIntoWords)
            {
                if (_listOfNegativeWords.Any(x => x.Equals(RemoveSymbolsAndNumbers(contentSplitIntoWord))))
                {
                    var filteredWord = new StringBuilder();
                    for (var i = 0; i < contentSplitIntoWord.Length; i++)
                    {
                        if (i == 0 || i == contentSplitIntoWord.Length - 1 || !char.IsLetter(contentSplitIntoWord[i])
                            || !char.IsLetter(contentSplitIntoWord[i + 1]))
                        {
                            filteredWord.Append(contentSplitIntoWord[i]);
                        }
                        else
                        {
                            filteredWord.Append('#');
                        }
                    }
                    filteredContent.Append(filteredWord);
                }
                else
                {
                    filteredContent.Append(contentSplitIntoWord);
                }
                if (count != contentSplitIntoWords.Count - 1)
                {
                    filteredContent.Append(' ');
                }
                count++;
            }
            return filteredContent.ToString();
        }

        private static string RemoveSymbolsAndNumbers(string x)
        {
            return Regex.Replace(x.ToLower(), @"[^A-Za-z]", string.Empty);
        }

        private static IEnumerable<string> SplitContentIntoLowerCaseWords(string content)
        {
            var contentSplitIntoLowerCaseWords = content.Split(' ');
            return contentSplitIntoLowerCaseWords;
        }
    }
}
