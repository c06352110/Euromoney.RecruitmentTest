using ContentConsole._ioc;

namespace ContentConsole.Filter
{
    public interface IFilterStrategy : IInjectable
    {
        int CountNegativeWords(string content);
        string FilterNegativeWords(string content, bool disabledFiltering);
    }
}