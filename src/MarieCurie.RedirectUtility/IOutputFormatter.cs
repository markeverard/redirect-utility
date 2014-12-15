using System.Collections.Generic;

namespace MarieCurie.RedirectUtility
{
    public interface IOutputFormatter
    {
        string ToString(IEnumerable<RedirectInstruction> redirectItems);
    }
}
