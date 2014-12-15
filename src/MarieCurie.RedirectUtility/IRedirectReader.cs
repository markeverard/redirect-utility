using System.Collections.Generic;

namespace MarieCurie.RedirectUtility
{
    public interface IRedirectReader
    {
        IEnumerable<RedirectInstruction> GetRedirectItems();
    }
}
