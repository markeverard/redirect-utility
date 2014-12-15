using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MarieCurie.RedirectUtility
{   
    public class WebRedirectorRequester : IWebRedirectRequester
    {
        public WebRedirectorRequester()
        {
            //Doesn't work. However run in Parallel with Fiddler and certificxate errors willbe ignored!
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };  
        }

        public IEnumerable<RedirectResult> ValidateInstructions(IEnumerable<RedirectInstruction> instructions)
        {
            var results = new List<RedirectResult>();
            var instructionsList = instructions.ToList();
            var instructionCount = instructionsList.Count;
            int i = 1;

            foreach (var instruction in instructionsList)
            {
                var request = (HttpWebRequest)WebRequest.Create(instruction.OldUrl);
                request.Method = "HEAD";
                request.Timeout = 10000;
                
                //request.AllowAutoRedirect = false;
                //request.MaximumAutomaticRedirections = 0;

                Console.WriteLine("Requesting {2}/{3} | {0} | {1}", request.Method, instruction.OldUrl, i, instructionCount);
                i++;
                
                var result = new RedirectResult { Instruction = instruction };
                try
                {

                    var response = (HttpWebResponse)request.GetResponse();

                    result.FinalDestinationUrl = response.ResponseUri.ToString();
                    result.ResponseStatusCode = response.StatusCode;
                }
                catch (WebException ex)
                {
                    var response = ex.Response;
                    
                    if (ex.Status != WebExceptionStatus.Timeout)
                        result.FinalDestinationUrl = response.ResponseUri.ToString();

                    result.ResponseStatusCode = ex.Status == WebExceptionStatus.ProtocolError
                        ? HttpStatusCode.NotFound
                        : HttpStatusCode.InternalServerError;

                    result.ExceptionMessage = ex.Message;
                }
                results.Add(result);
            }

            return results;
        }
    }
}