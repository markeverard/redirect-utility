using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using MarieCurie.RedirectUtility;

namespace RedirectTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputFileName = @"C:\Users\edmund.ward\Documents\redirect-utility\src\RedirectTestConsole\rewritemaps.config";
            const string outputFilename = "redirect";

            var reader = new ConfigFileReader(inputFileName);

            var urls = reader.GetSourceUrlsFromRewriteMap();

            ServicePointManager.ServerCertificateValidationCallback = new
                RemoteCertificateValidationCallback
                (
                delegate { return true; }
                );

            foreach (var url in urls)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //httpWebRequest.Method = WebRequestMethods.Http.Head;
                try
                {
                    var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                    Console.WriteLine(string.Format("Success: {0} {1}", url, httpResponse.StatusCode));
                }
                catch (Exception ex)
                {
                    var webException = ex as WebException;
                    Console.WriteLine(string.Format("Fail: {0} {1}", url, webException.Status));
                }
                
            }
            Console.ReadLine();
        }
    }
}
