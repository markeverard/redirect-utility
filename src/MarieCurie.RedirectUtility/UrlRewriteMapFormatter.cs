using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MarieCurie.RedirectUtility
{
    public class UrlRewriteMapFormatter : IOutputFormatter
    {
        private const string rewriteMapFormatter = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<rewriteMaps>\n\t<rewriteMap name=\"PermanentRedirects\">{0}</rewriteMap>\n</rewriteMaps>";

        public string ToString(IEnumerable<Redirect> redirectItems)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
         
            foreach (var item in redirectItems)
            {
                var oldUrl = item.OldUrl.Replace("&", "&amp;");
                var newUrl = item.NewUrl.Replace("&", "&amp;");
                
                sb.AppendFormat("\t<add key=\"{0}\" value=\"{1}\" />\n", oldUrl, newUrl);
            }

            return string.Format(rewriteMapFormatter, sb);
        }
    }
}
