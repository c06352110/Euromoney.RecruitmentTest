using System;
using System.Collections.Generic;
using ContentConsole.Filter;

namespace ContentConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var negativeWordsRepository = new NegativeWordRepository(new List<string>
            {
                "swine",
                "bad",
                "nasty",
                "horrible"
            });
            var manageContent = new ManageContent(new FilterBySplittingWordsStrategy(negativeWordsRepository, true));

            const string content = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";

            Console.WriteLine("Scanned the text:");
            Console.WriteLine(manageContent.FilterNegativeWords(content));
            Console.WriteLine($"Total Number of negative words: {manageContent.CountNegativeWords(content)}");

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }
    }

}
