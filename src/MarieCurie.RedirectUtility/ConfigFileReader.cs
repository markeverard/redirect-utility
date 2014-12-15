using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MarieCurie.RedirectUtility
{
    public class ConfigFileReader : IConfigFileReader
    {
        private readonly string _fileName;

        public ConfigFileReader(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<RedirectInstruction> GetInstructionsFromRewriteMap(int number = 10)
        {
            var configMap = XDocument.Load(_fileName);
            return
                configMap.Elements("rewriteMaps")
                    .Elements("rewriteMap")
                    .Elements("add")
                    .Select(e => new RedirectInstruction() { OldUrl = e.Attribute("key").Value, NewUrl = e.Attribute("value").Value})
                    .Take(number);
        }
    }
}
