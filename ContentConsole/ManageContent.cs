using ContentConsole.Filter;

namespace ContentConsole
{
    public class ManageContent
    {
        private readonly IFilterStrategy _filterStrategy;

        public ManageContent(IFilterStrategy filterStrategy)
        {
            _filterStrategy = filterStrategy;
        }

        public int CountNegativeWords(string content)
        {
            return _filterStrategy.CountNegativeWords(content);
        }

        public string FilterNegativeWords(string content)
        {
            return _filterStrategy.FilterNegativeWords(content);
        }
    }
}