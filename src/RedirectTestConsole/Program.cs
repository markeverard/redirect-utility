using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MarieCurie.RedirectUtility;

namespace RedirectTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputFileName = @"..\..\Data\rewriteMaps.Staging.config";
            //const string inputFileName = @"..\..\Data\rewriteMaps.Staging.test.config";
            const string badRedirectFile = @"..\..\Data\BadRedirectResults.txt";
            const string goodRedirectFile = @"..\..\Data\GoodRedirectResults.txt";
            const string logOutputFile = @"..\..\Data\ExecutionLog.txt";

            IConfigFileReader reader = new ConfigFileReader(inputFileName);
            IFilePersister badPersistor = new FilePersistor(badRedirectFile);
            IFilePersister goodPersistor = new FilePersistor(goodRedirectFile);

            var stopwatch = Stopwatch.StartNew();

            var instructions = reader.GetInstructionsFromRewriteMap(50000).ToList();

            var tester = new WebRedirectorRequester();
            var results = tester.ValidateInstructions(instructions).ToList();

            var goodResults = results.Where(r => r.State == RedirectResultState.Success).ToList();
            var badResults = results.Where(r => r.State != RedirectResultState.Success).ToList();

            // Save bad results
            var sb = new StringBuilder();
            foreach (var result in badResults)
                sb.AppendLine(result.ToString());

            badPersistor.Save(sb.ToString());

            // Save good results
            sb = new StringBuilder();
            foreach (var result in goodResults)
                sb.AppendLine(result.ToString());

            goodPersistor.Save(sb.ToString());

            stopwatch.Stop();
           
            var log = string.Format("Inspected {0} redirects | {1} SUCCESS | {2} FAILURE | {3} seconds", instructions.Count, goodResults.Count, badResults.Count, stopwatch.Elapsed.TotalSeconds);
            badPersistor = new FilePersistor(logOutputFile);
            badPersistor.Save(log);

            Console.WriteLine(log);
            Console.WriteLine("Hit any key to exit.");
            Console.ReadLine();
        }

        
    }
}
