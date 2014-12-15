using System.Net;

namespace MarieCurie.RedirectUtility
{
    public class RedirectResult
    {
        public RedirectInstruction Instruction { get; set; }
        public string FinalDestinationUrl { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ExceptionMessage { get; set; }

        public RedirectResultState State
        {
            get
            {
                if (ResponseStatusCode == HttpStatusCode.OK)
                {
                    return FinalDestinationUrl == Instruction.NewUrl 
                        ? RedirectResultState.Success 
                        : RedirectResultState.Error;
                }

                if (ResponseStatusCode == HttpStatusCode.Moved || ResponseStatusCode == HttpStatusCode.MovedPermanently)
                    return RedirectResultState.Loop;

                return RedirectResultState.Error;
            }
        }

        public override string ToString()
        {
            if (State == RedirectResultState.Success)
                return string.Format("SUCCESS - {0} redirected to {1} as expected", Instruction.OldUrl, FinalDestinationUrl);
            
            if (State == RedirectResultState.Loop)
                return string.Format("FAILURE - {0} redirected to {1} which caused a loop", Instruction.OldUrl, FinalDestinationUrl);

            return string.Format("FAILURE - {0} returned {1}", Instruction.OldUrl, ExceptionMessage);
        }
    }
}