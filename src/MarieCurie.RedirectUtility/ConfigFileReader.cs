using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarieCurie.RedirectUtility
{
    public class ConfigFileReader
    {
        private string _fileName;

        public ConfigFileReader(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<string> GetSourceUrlsFromRewriteMap()
        {
            var configMap = XDocument.Load(_fileName);
            return
                configMap.Elements("rewriteMaps")
                    .Elements("rewriteMap")
                    .Elements("add")
                    .Select(e => e.Attribute("key").Value).Take(10);
        }
    }
}
