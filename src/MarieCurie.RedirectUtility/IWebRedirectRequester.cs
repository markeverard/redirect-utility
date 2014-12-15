using System.Collections.Generic;

namespace MarieCurie.RedirectUtility
{
    public interface IWebRedirectRequester
    {
        IEnumerable<RedirectResult> ValidateInstructions(IEnumerable<RedirectInstruction> instructions);
    }
}