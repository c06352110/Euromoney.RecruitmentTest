using ContentConsole.Filter;

namespace ContentConsole
{
    public class FilterContext
    {
        private readonly IFilterStrategy _filterStrategy;

        public FilterContext(IFilterStrategy filterStrategy)
        {
            _filterStrategy = filterStrategy;
        }

        public string FilterNegativeWords(string content, bool disableFiltering)
        {
            return _filterStrategy.FilterNegativeWords(content, disableFiltering);
        }

        public int CountNegativeWords(string content)
        {
            return _filterStrategy.CountNegativeWords(content);
        }
    }
}