using System.IO;

namespace MarieCurie.RedirectUtility
{
    public class ConfigFilePersistor : IRedirectPersister
    {
        private readonly string _filePath;

        public ConfigFilePersistor(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(string formattedRedirects)
        {
            using (var writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(formattedRedirects);
            }
        }
    }
}