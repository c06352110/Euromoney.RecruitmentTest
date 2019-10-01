using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ContentConsole.Filter;

namespace ContentConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            var filterStrategy = container.Resolve<IFilterStrategy>();
            var filterContext = new FilterContext(filterStrategy);

            const string content = "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";

            Console.WriteLine("Scanned the text:");
            Console.WriteLine(filterContext.FilterNegativeWords(content, true));
            Console.WriteLine($"Total Number of negative words: {filterContext.CountNegativeWords(content)}");

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }
    }
}
