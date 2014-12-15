using System.IO;

namespace MarieCurie.RedirectUtility
{
    public class FilePersistor : IFilePersister
    {
        private readonly string _filePath;

        public FilePersistor(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(string stringToWrite)
        {
            using (var writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(stringToWrite);
            }
        }
    }
}