namespace ContentConsole.Filter
{
    public interface IFilterStrategy
    {
        int CountNegativeWords(string content);
        string FilterNegativeWords(string content);
    }
}