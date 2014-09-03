using System;
using System.Linq;
using MarieCurie.RedirectUtility;

namespace RedirectConsoleHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            const string outputFileName = "rewritemaps.config";
            
            IOutputFormatter formatter = new UrlRewriteMapFormatter();
            IRedirectReader reader = new CsvRedirectReader("shop.csv");
            IRedirectPersister persistor = new ConfigFilePersistor(outputFileName);

            var items = reader.GetRedirectItems().ToList();
            Console.WriteLine("Found {0} redirects", items.Count);
            
            var formattedOutput = formatter.ToString(items);
            persistor.Save(formattedOutput);

            Console.WriteLine("Written file to {0}. Use rewriteMaps in along with rewrite rules specified in exampleRules.config ", outputFileName);
            Console.ReadLine();
        }
    }
}
