using System.Collections.Generic;

namespace MarieCurie.RedirectUtility
{
    public interface IConfigFileReader
    {
        IEnumerable<RedirectInstruction> GetInstructionsFromRewriteMap(int number = 10);
    }
}